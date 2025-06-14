using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Models;
using ET_Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Services;

namespace ET_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, PasswordService passwordService, JwtService jwtService)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return BadRequest(new { message = "Email is already registered." });

        var hashedPassword = _passwordService.HashPassword(request.Password);

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            RoleId = 2 
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || user.PasswordHash == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var passwordValid = _passwordService.VerifyPassword(user.PasswordHash, request.Password);
        if (!passwordValid)
            return Unauthorized(new { message = "Invalid credentials" });

        var role = await _context.Roles.FindAsync(user.RoleId);
        var token = _jwtService.GenerateToken(user, role?.Name ?? "User");

        return Ok(new { token });
    }

}
