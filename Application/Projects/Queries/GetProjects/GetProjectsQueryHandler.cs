using Application.Abstractions;
using Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler
    : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectDto>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .Select(x => new ProjectDto(
                x.Id,
                x.Name,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}