using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ET_Backend.DTOs.BudgetsDTO;
using ET_Backend.Models.BudgetsModel;
using System.Security.Claims;

namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BudgetsController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<ActionResult> GetAllBudgets()
    {
        var budgets = await unitOfWork.Budgets.GetAllAsync();
        if (budgets == null)
            return NotFound("Data Not Found");
        var items = mapper.Map<List<BudgetsResDTO>>(budgets);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("getbudgetsbyid/{id}")]
    public async Task<ActionResult> GetBudgetsById(Guid id)
    {
        var budgets = await unitOfWork.Budgets.FirstOrDefaultAsync(itar => itar.Id == id);
        if (budgets == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<BudgetsResDTO>(budgets);
        return Ok(item);
    }
    [HttpGet("getbudgetsbyuserid/{userid}")]
    public async Task<ActionResult> GetBudgetssByUserId(Guid userid)
    {
        var budgets = await unitOfWork.Budgets.FindAsync(itar => itar.UserId == userid);
        if (budgets == null || !budgets.Any())
            return NotFound("No Budgets found for this user.");
        var item = mapper.Map<List<BudgetsResDTO>>(budgets);
        return Ok(item);
    }
    [HttpPost("createbudget")]
    public async Task<ActionResult> CreateBudgets([FromBody] BudgetsReqDTO req)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var budgets = mapper.Map<BudgetsModel>(req);
        budgets.UserId = userId;
        budgets.CreatedDate = budgets.ModifiedDate = DateTime.UtcNow;
        budgets.CreatedBy = budgets.ModifiedBy = userIdString;

        unitOfWork.Budgets.Add(budgets);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetBudgetsById), new { id = budgets.Id }, req);
    }
    [HttpPost("updatebudgets/{rowid}")]
    public async Task<IActionResult> UpdateBudgets([FromBody] BudgetsReqDTO req, Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid row ID.");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var returnVal = await unitOfWork.Budgets.UpdateBudgets(req, rowid, userIdString);
        if (!returnVal)
            return NotFound("Account not found or update failed.");
        return Ok("Account updated successfully.");
    }
    [HttpDelete("deletebudgets/{rowid}")]
    public async Task<IActionResult> DeleteBudgets(Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid account ID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not authorized.");

        var returnVal = await unitOfWork.Budgets.DeleteBudgets(rowid);
        if (!returnVal)
            return NotFound("Budgets not found or already deleted.");
        return Ok("Budgets deleted successfully.");
    }
}
