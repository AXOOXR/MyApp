using MediatR;
using Application.Common.Results;

namespace Application.Projects.Commands.DeactivateProject;

public record DeactivateProjectCommand(
    int Id
) : IRequest<Result>;