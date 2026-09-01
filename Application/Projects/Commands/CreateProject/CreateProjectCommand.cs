using MediatR;

namespace Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(string Name)
    : IRequest<int>;