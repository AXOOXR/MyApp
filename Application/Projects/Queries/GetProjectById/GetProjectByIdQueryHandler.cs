using Application.Abstractions;
using Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler
    : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProjectByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ProjectDto(
                x.Id,
                x.Name,
                x.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }
}