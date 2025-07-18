using ET_Backend.Data.IRepositories;
using ET_Backend.Data.IRepositories.IAuthRepo;
using ET_Backend.Data.IRepositories.IAccountsRepository;
using ET_Backend.Data.Repositories.AccountsRepository;
using ET_Backend.Data.IRepositories.ICategoriesRepository;
using ET_Backend.Data.Repositories.CategoriesRepository;
using ET_Backend.Data.IRepositories.IExpensesRepository;
using ET_Backend.Data.Repositories.ExpensesRepository;
using ET_Backend.Data.IRepositories.IIncomesRepository;
using ET_Backend.Data.Repositories.IncomesRepository;
using ET_Backend.Data.IRepositories.IBudgetsRepository;

namespace ET_Backend.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IAuthRepository Auth { get; private set; }
    public IAccountsRepo Accounts { get; private set; }
    public ICategoriesRepo Categories { get; private set; }
    public IExpensesRepo Expenses { get; private set; }
    public IIncomesRepo Incomes { get; private set; }
    public IBudgetsRepo Budgets { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Auth = new AuthRepository(_context);
        Accounts = new AccountsRepo(_context);
        Categories = new CategoriesRepo(_context);
        Expenses = new ExpensesRepo(_context);
        Incomes = new IncomesRepo(_context);
        Budgets = new BudgetsRepo(_context);
    }

    public int Complete() => _context.SaveChanges();

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}