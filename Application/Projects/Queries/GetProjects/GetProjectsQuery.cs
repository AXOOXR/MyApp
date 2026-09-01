using Application.Projects.DTOs;
using MediatR;

namespace Application.Projects.Queries.GetProjects;

public record GetProjectsQuery
    : IRequest<List<ProjectDto>>;