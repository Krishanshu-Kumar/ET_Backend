using ET_Backend.Data.IRepositories.IAuthRepo;
using ET_Backend.Data.IRepositories.IAccountsRepository;
using ET_Backend.Data.IRepositories.ICategoriesRepository;
using ET_Backend.Data.IRepositories.IExpensesRepository;
using ET_Backend.Data.IRepositories.IIncomesRepository;
using ET_Backend.Data.IRepositories.IBudgetsRepository;

namespace ET_Backend.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IAccountsRepo Accounts { get; }
    ICategoriesRepo Categories { get; }
    IExpensesRepo Expenses { get; }
    IIncomesRepo Incomes { get; }
    IBudgetsRepo Budgets { get; }
    int Complete();
    Task<int> CompleteAsync();
}
