using MediatR;
using Application.Common.Results;

namespace Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    int Id,
    string Name
) : IRequest<Result>;