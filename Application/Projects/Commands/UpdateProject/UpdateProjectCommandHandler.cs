using Application.Abstractions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Results;
namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        UpdateProjectCommand request,
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

        project.UpdateName(request.Name);

        _context.ProjectHistories.Add(
            new ProjectHistory(
                project.Id,
                "Project name updated"));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(1);
    }
}