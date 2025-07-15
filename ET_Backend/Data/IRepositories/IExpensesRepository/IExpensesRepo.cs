using ET_Backend.DTOs.ExpensesDTO;
using ET_Backend.Models.ExpensesModel;

namespace ET_Backend.Data.IRepositories.IExpensesRepository;

public interface IExpensesRepo : IRepository<ExpensesModel>
{
    Task<bool> UpdateExpenses(ExpensesReqDTO req, Guid rowid, string userid);
    Task<bool> DeleteExpenses(Guid id, string userId);
}