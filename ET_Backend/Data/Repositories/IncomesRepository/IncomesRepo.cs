using ET_Backend.Data.IRepositories.IIncomesRepository;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Models.IncomesModel;
using ET_Backend.DTOs.IncomesDTO;

namespace ET_Backend.Data.Repositories.IncomesRepository;

public class IncomesRepo : Repository<IncomesModel>, IIncomesRepo
{
    private readonly AppDbContext dBcontext;

    public IncomesRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    public async Task<bool> UpdateIncomes(IncomesReqDTO req, Guid rowid, string userid)
    {
        var result = await dBcontext.Incomes
            .Where(prop => prop.Id == rowid)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.UserId, p => req.UserId)
                .SetProperty(p => p.AccountId, p => req.AccountId)
                .SetProperty(p => p.CatgoryId, p => req.CatgoryId)
                .SetProperty(p => p.Amount, p => req.Amount)
                .SetProperty(p => p.Currency, p => req.Currency)
                .SetProperty(p => p.Description, p => req.Description)
                .SetProperty(p => p.IncomeDate, p => req.IncomeDate)
                .SetProperty(p => p.ModifiedBy, p => userid)
                .SetProperty(p => p.ModifiedDate, p => DateTime.UtcNow)
            );
        return result > 0;
    }
    public async Task<bool> DeleteIncomes(Guid id, string userId)
    {
        var incomes = await dBcontext.Incomes.FirstOrDefaultAsync(a => a.Id == id);

        if (incomes == null)
            return false;

        dBcontext.Incomes.Remove(incomes);
        await dBcontext.SaveChangesAsync();
        return true;
    }
}