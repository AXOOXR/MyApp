using MediatR;

namespace Application.Projects.Commands.ActivateProject;

public record ActivateProjectCommand(
    int Id
) : IRequest<bool>;