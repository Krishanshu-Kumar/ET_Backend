using System.Threading.Tasks;
using ET_Backend.Data.IRepositories.IAuthRepo;
using ET_Backend.Data.IRepositories.IAccountsRepository;
using ET_Backend.Data.IRepositories;
using ET_Backend.Data.Repositories.AccountsRepository;

namespace ET_Backend.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IAuthRepository Auth { get; private set; }
    public IAccountsRepo Accounts { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Auth = new AuthRepository(_context);
        Accounts = new AccountsRepo(_context);
    }

    public int Complete() => _context.SaveChanges();

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}