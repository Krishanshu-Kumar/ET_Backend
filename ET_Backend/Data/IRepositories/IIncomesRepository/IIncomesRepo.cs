using ET_Backend.DTOs.IncomesDTO;
using ET_Backend.Models.IncomesModel;

namespace ET_Backend.Data.IRepositories.IIncomesRepository;

public interface IIncomesRepo : IRepository<IncomesModel>
{
    Task<bool> UpdateIncomes(IncomesReqDTO req, Guid rowid, string userid);
    Task<bool> DeleteIncomes(Guid id, string userId);
}