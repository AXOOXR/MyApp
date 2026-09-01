using MediatR;

namespace Application.Projects.Commands.DeactivateProject;

public record DeactivateProjectCommand(
    int Id
) : IRequest<bool>;