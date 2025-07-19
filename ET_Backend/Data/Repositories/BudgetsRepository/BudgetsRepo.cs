using Microsoft.EntityFrameworkCore;
using ET_Backend.Models.BudgetsModel;
using ET_Backend.Data.IRepositories.IBudgetsRepository;
using ET_Backend.DTOs.BudgetsDTO;

namespace ET_Backend.Data.Repositories.CategoriesRepository;
public class BudgetsRepo : Repository<BudgetsModel>, IBudgetsRepo
{
    private readonly AppDbContext dBcontext;

    public BudgetsRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    public async Task<bool> UpdateBudgets(BudgetsReqDTO req, Guid rowid, string userid)
    {
        var result = await dBcontext.Budgets
            .Where(prop => prop.Id == rowid)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.UserId, p => req.UserId)
                .SetProperty(p => p.CategoryID, p => req.CategoryID)
                .SetProperty(p => p.Amount, p => req.Amount)
                .SetProperty(p => p.Currency, p => req.Currency)
                .SetProperty(p => p.Month, p => req.Month)
                .SetProperty(p => p.Year, p => req.Year)
                .SetProperty(p => p.ModifiedBy, p => userid)
                .SetProperty(p => p.ModifiedDate, p => DateTime.UtcNow)
            );
        return result > 0;
    }
    public async Task<bool> DeleteBudgets(Guid rowid)
    {
        var budgets = await dBcontext.Budgets.FirstOrDefaultAsync(a => a.Id == rowid);

        if (budgets == null)
            return false;

        dBcontext.Budgets.Remove(budgets);
        await dBcontext.SaveChangesAsync();
        return true;
    }
}