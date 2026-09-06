using Application.Abstractions;
using Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler
    : IRequestHandler<DeleteProjectCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        DeleteProjectCommand request,
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

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(1);
    }
}