using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ET_Backend.DTOs.AccountsDTO;
using ET_Backend.Models.AccountsModel;
using System.Security.Claims;

namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountsController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{

    [HttpGet("getall")]
    public async Task<ActionResult> GetAllAccounts()
    {
        var accounts = await unitOfWork.Accounts.GetAllAsync();
        if (accounts == null)
            return NotFound("Data Not Found");
        var items = mapper.Map<List<AccountsResDTO>>(accounts);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("getaccountsbyid/{id}")]
    public async Task<ActionResult> GetAccountsById(Guid id)
    {
        var accounts = await unitOfWork.Accounts.FirstOrDefaultAsync(itar => itar.Id == id);
        if (accounts == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<AccountsResDTO>(accounts);
        return Ok(item);
    }
    [HttpGet("getaccountsbyuserid/{userid}")]
    public async Task<ActionResult> GetAccountsByUserId(Guid userid)
    {
        var accounts = await unitOfWork.Accounts.FindAsync(itar => itar.UserId == userid);
        if (accounts == null || !accounts.Any())
            return NotFound("No accounts found for this user.");
        var item = mapper.Map<List<AccountsResDTO>>(accounts);
        return Ok(item);
    }
    [HttpPost("createaccount")]
    public async Task<ActionResult> CreateAccount([FromBody] AccountsReqDTO req)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var account = mapper.Map<AccountsModel>(req);
        account.UserId = userId;
        account.CreatedDate = account.ModifiedDate = DateTime.UtcNow;
        account.CreatedBy = account.ModifiedBy = userIdString;

        unitOfWork.Accounts.Add(account);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetAccountsById), new { id = account.Id }, req);
    }
    [HttpPost("updateaccount/{rowid}")]
    public async Task<IActionResult> UpdateAccount([FromBody] AccountsReqDTO req, Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid row ID.");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var returnVal = await unitOfWork.Accounts.UpdateAccount(req, rowid, userIdString);
        if (!returnVal)
            return NotFound("Account not found or update failed.");
        return Ok("Account updated successfully.");
    }
    [HttpDelete("deleteexpenses/{rowid}")]
    public async Task<IActionResult> DeleteExpenses(Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid account ID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not authorized.");

        var returnVal = await unitOfWork.Expenses.DeleteExpenses(rowid, userId);
        if (!returnVal)
            return NotFound("Account not found or already deleted.");
        return Ok("Account deleted successfully.");
    }
}
