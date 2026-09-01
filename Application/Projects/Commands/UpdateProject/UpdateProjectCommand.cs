using MediatR;

namespace Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(
    int Id,
    string Name
) : IRequest<bool>;