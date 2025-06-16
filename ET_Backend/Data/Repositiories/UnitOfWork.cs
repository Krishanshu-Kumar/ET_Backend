// File: Data/UnitOfWork/UnitOfWork.cs

// public class UnitOfWork : IUnitOfWork
// {
//     private readonly YourDbContext _context;

//     public IRepo<YourEntity> YourEntityRepo { get; }

//     public UnitOfWork(YourDbContext context)
//     {
//         _context = context;
//         YourEntityRepo = new Repo<YourEntity>(_context);
//     }

//     public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

//     public void Dispose() => _context.Dispose();
// }
