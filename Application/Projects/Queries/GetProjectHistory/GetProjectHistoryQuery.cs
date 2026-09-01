using Application.Projects.DTOs;
using MediatR;

namespace Application.Projects.Queries.GetProjectHistory;

public record GetProjectHistoryQuery(
    int ProjectId
) : IRequest<List<ProjectHistoryDto>>;