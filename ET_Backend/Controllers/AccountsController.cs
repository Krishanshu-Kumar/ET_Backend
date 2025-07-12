using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Models;
using ET_Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Services;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ET_Backend.DTOs.AccountsDTO;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Http.HttpResults;
using ET_Backend.Models.AccountsModel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountsController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{

    [HttpGet("/getall")]
    public async Task<ActionResult> GetAllAccounts()
    {
        var accounts = await unitOfWork.Accounts.GetAllAsync();
        if (accounts == null)
            return NotFound("Data Not Found");
        var items = mapper.Map<List<AccountsResDTO>>(accounts);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("/getaccountsbyid/{id}")]
    public async Task<ActionResult> GetAccountsById(Guid id)
    {
        var accounts = await unitOfWork.Accounts.FirstOrDefaultAsync(itar => itar.Id == id);
        if (accounts == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<AccountsResDTO>(accounts);
        return Ok(item);
    }
    [HttpPost("createaccount")]
    public async Task<ActionResult> CreateAccount([FromBody] AccountsReqDTO req)
    {
        Console.WriteLine($"User authenticated: {User.Identity?.IsAuthenticated}");

        // 🔍 Dump all claims for debugging
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type} | Value: {claim.Value}");
        }

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var account = mapper.Map<AccountsModel>(req);
        account.UserId = userId;
        account.CreatedDate = account.ModifiedDate = DateTime.UtcNow;
        account.CreatedBy = account.ModifiedBy = userIdString;

        unitOfWork.Accounts.Add(account);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAccountsById), new { id = account.Id }, account);
    }
}
