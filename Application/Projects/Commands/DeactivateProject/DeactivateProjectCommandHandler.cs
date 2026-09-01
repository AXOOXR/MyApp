using Application.Abstractions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Commands.DeactivateProject;

public class DeactivateProjectCommandHandler
    : IRequestHandler<DeactivateProjectCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeactivateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeactivateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (project is null)
            return false;

        project.Deactivate();

        _context.ProjectHistories.Add(
            new ProjectHistory(
                project.Id,
                "Project deactivated"));

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}