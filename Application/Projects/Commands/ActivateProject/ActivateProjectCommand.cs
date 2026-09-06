using MediatR;
using Application.Common.Results;
namespace Application.Projects.Commands.ActivateProject;


public record ActivateProjectCommand(
    int Id
) : IRequest<Result>;