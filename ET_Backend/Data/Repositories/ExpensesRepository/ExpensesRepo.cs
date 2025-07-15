using ET_Backend.Data.IRepositories.IExpensesRepository;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Models.ExpensesModel;
using ET_Backend.DTOs.ExpensesDTO;
// using ET_Backend.DTOs.ExpensesDTO;

namespace ET_Backend.Data.Repositories.ExpensesRepository;

public class ExpensesRepo : Repository<ExpensesModel>, IExpensesRepo
{
    private readonly AppDbContext dBcontext;

    public ExpensesRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    public async Task<bool> UpdateExpenses(ExpensesReqDTO req, Guid rowid, string userid)
    {
        var result = await dBcontext.Expenses
            .Where(prop => prop.Id == rowid)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.UserId, p => req.UserId)
                .SetProperty(p => p.AccountId, p => req.AccountId)
                .SetProperty(p => p.CatgoryId, p => req.CatgoryId)
                .SetProperty(p => p.Amount, p => req.Amount)
                .SetProperty(p => p.Currency, p => req.Currency)
                .SetProperty(p => p.Description, p => req.Description)
                .SetProperty(p => p.ExpenseDate, p => req.ExpenseDate)
                .SetProperty(p => p.ModifiedBy, p => userid)
                .SetProperty(p => p.ModifiedDate, p => DateTime.UtcNow)
            );
        return result > 0;
    }
    public async Task<bool> DeleteExpenses(Guid id, string userId)
    {
        var expenses = await dBcontext.Expenses.FirstOrDefaultAsync(a => a.Id == id);

        if (expenses == null)
            return false;

        dBcontext.Expenses.Remove(expenses);
        await dBcontext.SaveChangesAsync();
        return true;
    }
}
