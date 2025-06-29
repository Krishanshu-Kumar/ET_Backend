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

namespace ET_Backend.Controllers;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class AccountsController(IUnitOfWork unitOfWork,AppDbContext context, IMapper mapper) : ControllerBase
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
}
