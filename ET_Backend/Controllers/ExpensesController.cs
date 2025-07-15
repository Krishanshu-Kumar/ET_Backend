using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Security.Claims;
using ET_Backend.DTOs.ExpensesDTO;
using ET_Backend.Models.ExpensesModel;

namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ExpensesController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{

    [HttpGet("getall")]
    public async Task<ActionResult> GetAllExpenses()
    {
        var expenses = await unitOfWork.Expenses.GetAllAsync();
        if (expenses == null || !expenses.Any())
            return NotFound("Data Not Found");
        var items = mapper.Map<List<ExpensesResDTO>>(expenses);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("getexpenses/{id}")]
    public async Task<ActionResult> GetExpensesById(Guid id)
    {
        var expenses = await unitOfWork.Expenses.FirstOrDefaultAsync(itar => itar.Id == id);
        if (expenses == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<ExpensesResDTO>(expenses);
        return Ok(item);
    }
    [HttpGet("getexpensesbyuserid/{userid}")]
    public async Task<ActionResult> GetExpensesByUserId(Guid userid)
    {
        var expenses = await unitOfWork.Expenses.FindAsync(itar => itar.UserId == userid);
        if (expenses == null || !expenses.Any())
            return NotFound("Data Not Found");
        var item = mapper.Map<List<ExpensesResDTO>>(expenses);
        return Ok(item);
    }
    [HttpGet("getexpensesbyaccountid/{accountid}")]
    public async Task<ActionResult> GetExpensessByAccountId(Guid accountid)
    {
        var expenses = await unitOfWork.Expenses.FindAsync(itar => itar.AccountId == accountid);
        if (expenses == null || !expenses.Any())
            return NotFound("Data Not Found");
        var item = mapper.Map<List<ExpensesResDTO>>(expenses);
        return Ok(item);
    }
    [HttpPost("createexpense")]
    public async Task<ActionResult> CreateExpenses([FromBody] ExpensesReqDTO req)
    {
        var existingUserid = await unitOfWork.Auth.FirstOrDefaultAsync(itar => itar.Id == req.UserId);
        if (existingUserid == null)
            return BadRequest("No such user exists!");

        var existingAccountid = await unitOfWork.Accounts.FirstOrDefaultAsync(itar => itar.Id == req.AccountId);
        if (existingAccountid == null)
            return BadRequest("No such Account of the User exists!");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var expense = mapper.Map<ExpensesModel>(req);
        expense.UserId = userId;
        expense.CreatedDate = expense.ModifiedDate = DateTime.UtcNow;
        expense.CreatedBy = expense.ModifiedBy = userIdString;

        unitOfWork.Expenses.Add(expense);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetExpensesById), new { id = expense.Id }, req);
    }
    [HttpPost("updateexpenses/{rowid}")]
    public async Task<IActionResult> UpdateExpenses([FromBody] ExpensesReqDTO req, Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid row ID.");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var returnVal = await unitOfWork.Expenses.UpdateExpenses(req, rowid, userIdString);
        if (!returnVal)
            return NotFound("Account not found or update failed.");
        return Ok("Account updated successfully.");
    }
    // [HttpDelete("deleteaccount/{rowid}")]
    // public async Task<IActionResult> DeleteAccount(Guid rowid)
    // {
    //     if (rowid == Guid.Empty)
    //         return BadRequest("Invalid account ID.");

    //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     if (string.IsNullOrEmpty(userId))
    //         return Unauthorized("User not authorized.");

    //     var returnVal = await unitOfWork.Accounts.DeleteAccount(rowid, userId);
    //     if (!returnVal)
    //         return NotFound("Account not found or already deleted.");
    //     return Ok("Account deleted successfully.");
// }
}
