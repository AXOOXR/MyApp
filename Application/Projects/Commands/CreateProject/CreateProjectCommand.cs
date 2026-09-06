using MediatR;
using Application.Common.Results;

namespace Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(string Name)
    : IRequest<Result>;