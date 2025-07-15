using AutoMapper;
using ET_Backend.DTOs.AccountsDTO;
using ET_Backend.DTOs.CategoriesDTO;
using ET_Backend.DTOs.ExpensesDTO;
using ET_Backend.Models.AccountsModel;
using ET_Backend.Models.CategoriesModel;
using ET_Backend.Models.ExpensesModel;

namespace ET_Backend.Helpers;

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

    }
}
