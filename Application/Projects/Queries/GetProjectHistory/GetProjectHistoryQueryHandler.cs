using Application.Abstractions;
using Application.Projects.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectHistory;

public class GetProjectHistoryQueryHandler
    : IRequestHandler<
        GetProjectHistoryQuery,
        List<ProjectHistoryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectHistoryQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectHistoryDto>> Handle(
        GetProjectHistoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ProjectHistories
            .AsNoTracking()
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ProjectHistoryDto(
                x.Id,
                x.ProjectId,
                x.Action,
                x.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}