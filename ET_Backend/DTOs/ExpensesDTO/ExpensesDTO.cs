namespace ET_Backend.DTOs.ExpensesDTO;

public record ExpensesResDTO(  
    Guid Id ,
    Guid UserId ,
    Guid AccountId ,
    int CatgoryId ,
    decimal Amount ,
    string Currency,
    string Description,
    DateTime ExpenseDate
);

public record ExpensesReqDTO(
    Guid UserId ,
    Guid AccountId ,
    int CatgoryId ,
    decimal Amount ,
    string Currency,
    string Description,
    DateTime ExpenseDate
);