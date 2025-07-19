namespace ET_Backend.DTOs.BudgetsDTO;

public record BudgetsResDTO(
    Guid Id,
    Guid UserId,
    int CategoryID,
    decimal Amount,
    string Currency,
    int Month,
    int Year
);

public record BudgetsReqDTO(
    Guid UserId,
    int CategoryID,
    decimal Amount,
    string Currency,
    int Month,
    int Year
);