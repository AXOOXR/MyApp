namespace Application.Projects.DTOs;

public record ProjectHistoryDto(
    int Id,
    int ProjectId,
    string Action,
    DateTime CreatedAt
);