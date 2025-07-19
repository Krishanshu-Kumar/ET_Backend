using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Security.Claims;
using ET_Backend.DTOs.IncomesDTO;
using ET_Backend.Models.IncomesModel;

namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class IncomesController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{

    [HttpGet("getall")]
    public async Task<ActionResult> GetAllIncomes()
    {
        var incomes = await unitOfWork.Incomes.GetAllAsync();
        if (incomes == null || !incomes.Any())
            return NotFound("Data Not Found");
        var items = mapper.Map<List<IncomesResDTO>>(incomes);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("getincomes/{id}")]
    public async Task<ActionResult> GetIncomesById(Guid id)
    {
        var incomes = await unitOfWork.Incomes.FirstOrDefaultAsync(itar => itar.Id == id);
        if (incomes == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<IncomesResDTO>(incomes);
        return Ok(item);
    }
    [HttpGet("getincomesbyuserid/{userid}")]
    public async Task<ActionResult> GetincomesByUserId(Guid userid)
    {
        var incomes = await unitOfWork.Incomes.FindAsync(itar => itar.UserId == userid);
        if (incomes == null || !incomes.Any())
            return NotFound("Data Not Found");
        var item = mapper.Map<List<IncomesResDTO>>(incomes);
        return Ok(item);
    }
    [HttpGet("getincomesbyaccountid/{accountid}")]
    public async Task<ActionResult> GetIncomesByAccountId(Guid accountid)
    {
        var incomes = await unitOfWork.Incomes.FindAsync(itar => itar.AccountId == accountid);
        if (incomes == null || !incomes.Any())
            return NotFound("Data Not Found");
        var item = mapper.Map<List<IncomesResDTO>>(incomes);
        return Ok(item);
    }
    [HttpPost("createincomes")]
    public async Task<ActionResult> CreateIncomes([FromBody] IncomesReqDTO req)
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

        var incomes = mapper.Map<IncomesModel>(req);
        incomes.UserId = userId;
        incomes.CreatedDate = incomes.ModifiedDate = DateTime.UtcNow;
        incomes.CreatedBy = incomes.ModifiedBy = userIdString;

        unitOfWork.Incomes.Add(incomes);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetIncomesById), new { id = incomes.Id }, req);
    }
    [HttpPost("updateincomes/{rowid}")]
    public async Task<IActionResult> UpdateIncomes([FromBody] IncomesReqDTO req, Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid row ID.");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var returnVal = await unitOfWork.Incomes.UpdateIncomes(req, rowid, userIdString);
        if (!returnVal)
            return NotFound("Incomes not found or update failed.");
        return Ok("Incomes updated successfully.");
    }
    [HttpDelete("deleteincomes/{rowid}")]
    public async Task<IActionResult> DeleteIncomes(Guid rowid)
    {
        if (rowid == Guid.Empty)
            return BadRequest("Invalid account ID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not authorized.");

        var returnVal = await unitOfWork.Incomes.DeleteIncomes(rowid, userId);
        if (!returnVal)
            return NotFound("Incomes not found or already deleted.");
        return Ok("Incomes deleted successfully.");
    }
}