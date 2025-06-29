namespace ET_Backend.DTOs.AccountsDTO;

public record AccountsResDTO(
    Guid Id,
    Guid UserId,
    string Name,
    string Type,
    decimal Balance,
    string Currency
);