using ET_Backend.DTOs.CategoriesDTO;
using ET_Backend.Models.CategoriesModel;

namespace ET_Backend.Data.IRepositories.ICategoriesRepository;

public interface ICategoriesRepo : IRepository<CategoriesModel>
{
    Task<bool> UpdateCategories(CategoriesReqDTO req, int rowid, string userid);
    Task<bool> DeleteCategories(int rowid);
}

