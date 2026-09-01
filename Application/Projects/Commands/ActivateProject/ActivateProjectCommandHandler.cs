using Application.Abstractions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Commands.ActivateProject;

public class ActivateProjectCommandHandler
    : IRequestHandler<ActivateProjectCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ActivateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        ActivateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (project is null)
            return false;

        project.Activate();

        _context.ProjectHistories.Add(
            new ProjectHistory(
                project.Id,
                "Project activated"));

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}