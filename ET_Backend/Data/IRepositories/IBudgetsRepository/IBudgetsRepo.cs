using ET_Backend.DTOs.BudgetsDTO;
using ET_Backend.Models.BudgetsModel;

namespace ET_Backend.Data.IRepositories.IBudgetsRepository;

public interface IBudgetsRepo : IRepository<BudgetsModel>
{
    Task<bool> UpdateBudgets(BudgetsReqDTO req, Guid rowid, string userid);
    Task<bool> DeleteBudgets(Guid rowid);
}

