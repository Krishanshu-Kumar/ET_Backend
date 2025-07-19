namespace ET_Backend.DTOs.IncomesDTO;

public record IncomesResDTO(  
    Guid Id ,
    Guid UserId ,
    Guid AccountId ,
    int CatgoryId ,
    decimal Amount ,
    string Currency,
    string Description,
    DateTime IncomeDate
);

public record IncomesReqDTO(
    Guid UserId ,
    Guid AccountId ,
    int CatgoryId ,
    decimal Amount ,
    string Currency,
    string Description,
    DateTime IncomeDate
);