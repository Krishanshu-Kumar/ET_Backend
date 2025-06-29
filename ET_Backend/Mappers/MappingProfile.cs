using AutoMapper;
using ET_Backend.Models;
using ET_Backend.DTOs;
using ET_Backend.DTOs.AccountsDTO;
using ET_Backend.Models.AccountsModel;

namespace ET_Backend.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountsModel, AccountsResDTO>().ReverseMap();

    }
}
