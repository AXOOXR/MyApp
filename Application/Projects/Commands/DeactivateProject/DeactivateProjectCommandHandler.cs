using Application.Abstractions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Results;
namespace Application.Projects.Commands.DeactivateProject;

public class DeactivateProjectCommandHandler
    : IRequestHandler<DeactivateProjectCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeactivateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        DeactivateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (project is null)
        {
            return Result.Failure(
                new Error(
                    "Project.NotFound",
                    "Project was not found."));
        }

        project.Deactivate();

        _context.ProjectHistories.Add(
            new ProjectHistory(
                project.Id,
                "Project deactivated"));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(1);
    }
}