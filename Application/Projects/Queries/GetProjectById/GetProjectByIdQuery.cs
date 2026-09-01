using Application.Projects.DTOs;
using MediatR;

namespace Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(
    int Id
) : IRequest<ProjectDto?>;