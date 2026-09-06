using MediatR;
using Application.Common.Results;

namespace Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id)
    : IRequest<Result>;