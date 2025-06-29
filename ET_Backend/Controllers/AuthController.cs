using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Models;
using ET_Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Services;
using ET_Backend.Data.IRepositories;
namespace ET_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUnitOfWork unitOfWork,AppDbContext context, PasswordService passwordService, JwtService jwtService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await unitOfWork.Auth.SingleOrDefaultAsync(itar => itar.Email == request.Email);
        // var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            return BadRequest(new { message = "Email is already registered." });

        var hashedPassword = passwordService.HashPassword(request.Password);

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            RoleId = 2 
        };

        context.Users.Add(newUser);
        await unitOfWork.CompleteAsync();

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || user.PasswordHash == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var passwordValid = passwordService.VerifyPassword(user.PasswordHash, request.Password);
        if (!passwordValid)
            return Unauthorized(new { message = "Invalid credentials" });

        var role = await context.Roles.FindAsync(user.RoleId);
        var token = jwtService.GenerateToken(user, role?.Name ?? "User");

        return Ok(new { token });
    }

}
