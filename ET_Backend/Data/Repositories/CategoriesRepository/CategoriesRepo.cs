using Microsoft.EntityFrameworkCore;
using ET_Backend.Models.CategoriesModel;
using ET_Backend.Data.IRepositories.ICategoriesRepository;
using ET_Backend.DTOs.CategoriesDTO;

namespace ET_Backend.Data.Repositories.CategoriesRepository;
public class CategoriesRepo : Repository<CategoriesModel>, ICategoriesRepo
{
    private readonly AppDbContext dBcontext;

    public CategoriesRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    public async Task<bool> UpdateCategories(CategoriesReqDTO req, int rowid, string userid)
    {
        var result = await dBcontext.Categories
            .Where(prop => prop.Id == rowid)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Name, p => req.Name)
                .SetProperty(p => p.Type, p => req.Type)
                .SetProperty(p => p.Icon, p => req.Icon)
                .SetProperty(p => p.Color, p => req.Color)
                .SetProperty(p => p.ModifiedBy, p => userid)
                .SetProperty(p => p.ModifiedDate, p => DateTime.UtcNow)
            );
        return result > 0;
    }
    public async Task<bool> DeleteCategories(int rowid)
    {
        var categories = await dBcontext.Categories.FirstOrDefaultAsync(a => a.Id == rowid);

        if (categories == null)
            return false;

        dBcontext.Categories.Remove(categories);
        await dBcontext.SaveChangesAsync();
        return true;
    }
}
