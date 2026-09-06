using Application.Abstractions;
using Domain;
using MediatR;
using Application.Common.Results;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = new Project(request.Name);

        if (project is null)
        {
            return Result.Failure(
                new Error(
                    "Project.NotFound",
                    "Project was not found."));
        }

        _context.Projects.Add(project);

        await _context.SaveChangesAsync(cancellationToken);

        var history = new ProjectHistory(
            project.Id,
            "Project created");

        _context.ProjectHistories.Add(history);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(project.Id);

    }
}