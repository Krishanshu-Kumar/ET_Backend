namespace ET_Backend.DTOs.CategoriesDTO;

public record CategoriesResDTO(
    
    int Id,
    string Name,
    string Type,
    string Icon,
    string Color 
);

public record CategoriesReqDTO(
    string Name,
    string Type,
    string Icon,
    string Color 
);