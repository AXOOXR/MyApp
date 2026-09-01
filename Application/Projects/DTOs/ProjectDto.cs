namespace Application.Projects.DTOs;

public record ProjectDto(
    int Id,
    string Name,
    bool IsActive
);