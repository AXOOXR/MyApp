using Application.Abstractions;
using Domain;
using MediatR;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = new Project(request.Name);

        _context.Projects.Add(project);

        await _context.SaveChangesAsync(cancellationToken);

        var history = new ProjectHistory(
            project.Id,
            "Project created");

        _context.ProjectHistories.Add(history);

        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}