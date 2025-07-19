using Microsoft.EntityFrameworkCore;
using ET_Backend.Models;
using ET_Backend.Models.AccountsModel;
using ET_Backend.Models.CategoriesModel;
using ET_Backend.Models.ExpensesModel;
using ET_Backend.Models.IncomesModel;
using ET_Backend.Models.BudgetsModel;

namespace ET_Backend.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region Authorization
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    #endregion

    #region Accounts
    public DbSet<AccountsModel> Accounts => Set<AccountsModel>();
    public DbSet<CategoriesModel> Categories => Set<CategoriesModel>();
    #endregion

    #region Transactions - Expenses/Incomes
    public DbSet<ExpensesModel> Expenses => Set<ExpensesModel>();
    public DbSet<IncomesModel> Incomes => Set<IncomesModel>();
    #endregion

    public DbSet<BudgetsModel> Budgets => Set<BudgetsModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        );
    }
}
