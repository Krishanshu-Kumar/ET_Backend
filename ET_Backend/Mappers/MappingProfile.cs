using AutoMapper;
using ET_Backend.DTOs.AccountsDTO;
using ET_Backend.DTOs.BudgetsDTO;
using ET_Backend.DTOs.CategoriesDTO;
using ET_Backend.DTOs.ExpensesDTO;
using ET_Backend.DTOs.IncomesDTO;
using ET_Backend.Models.AccountsModel;
using ET_Backend.Models.BudgetsModel;
using ET_Backend.Models.CategoriesModel;
using ET_Backend.Models.ExpensesModel;
using ET_Backend.Models.IncomesModel;

namespace ET_Backend.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Accounts
        CreateMap<AccountsModel, AccountsResDTO>().ReverseMap();
        CreateMap<AccountsModel, AccountsReqDTO>().ReverseMap();
        #endregion

        #region Categories
        CreateMap<CategoriesModel, CategoriesResDTO>().ReverseMap();
        CreateMap<CategoriesModel, CategoriesReqDTO>().ReverseMap();
        #endregion

        #region Expenses
        CreateMap<ExpensesModel, ExpensesReqDTO>().ReverseMap();
        CreateMap<ExpensesModel, ExpensesResDTO>().ReverseMap();
        #endregion

        #region Incomes
        CreateMap<IncomesModel, IncomesReqDTO>().ReverseMap();
        CreateMap<IncomesModel, IncomesResDTO>().ReverseMap();
        #endregion

        #region Budgets
        CreateMap<BudgetsModel, BudgetsReqDTO>().ReverseMap();
        CreateMap<BudgetsModel, BudgetsResDTO>().ReverseMap();
        #endregion
    }
}
