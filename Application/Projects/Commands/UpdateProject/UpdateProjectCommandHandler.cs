using Application.Abstractions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (project is null)
            return false;

        project.UpdateName(request.Name);

        _context.ProjectHistories.Add(
            new ProjectHistory(
                project.Id,
                "Project name updated"));

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}