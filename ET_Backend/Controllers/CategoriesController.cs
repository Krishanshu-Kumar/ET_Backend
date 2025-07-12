using Microsoft.AspNetCore.Mvc;
using ET_Backend.Data;
using ET_Backend.Data.IRepositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ET_Backend.DTOs.CategoriesDTO;
using System.Security.Claims;
using ET_Backend.Models.CategoriesModel;

namespace ET_Backend.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoriesController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<ActionResult> GetAllCategories()
    {
        var categories = await unitOfWork.Categories.GetAllAsync();
        if (categories == null)
            return NotFound("Data Not Found");
        var items = mapper.Map<List<CategoriesResDTO>>(categories);
        return Ok(items.OrderBy(itar => itar.Id));
    }
    [HttpGet("getcategoriesbyid/{id}")]
    public async Task<ActionResult> GetCategoriesById(int id)
    {
        var categories = await unitOfWork.Categories.FirstOrDefaultAsync(itar => itar.Id == id);
        if (categories == null)
            return NotFound("Data Not Found");
        var item = mapper.Map<CategoriesResDTO>(categories);
        return Ok(item);
    }
    [HttpPost("createcategories")]
    public async Task<ActionResult> CreateCategories([FromBody] CategoriesReqDTO req)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var categories = mapper.Map<CategoriesModel>(req);
        categories.CreatedDate = categories.ModifiedDate = DateTime.UtcNow;
        categories.CreatedBy = categories.ModifiedBy = userIdString;

        unitOfWork.Categories.Add(categories);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetCategoriesById), new { id = categories.Id }, req);
    }
    [HttpPost("updatecategories/{rowid}")]
    public async Task<IActionResult> UpdateCategories([FromBody] CategoriesReqDTO req, int rowid)
    {
        if (rowid == null)
            return BadRequest("Invalid row ID.");

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized("Invalid user ID in token.");

        var returnVal = await unitOfWork.Categories.UpdateCategories(req, rowid, userIdString);
        if (!returnVal)
            return NotFound("Category not found or update failed.");
        return Ok("Category updated successfully.");
    }
    [HttpDelete("deletecategory/{rowid}")]
    public async Task<IActionResult> DeleteCategory(int rowid)
    {
        if (rowid == 0)
            return BadRequest("Invalid ID.");

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not authorized.");

        var returnVal = await unitOfWork.Categories.DeleteCategories(rowid);
        if (!returnVal)
            return NotFound("Category not found or already deleted.");
        return Ok("Category deleted successfully.");
    }
}
