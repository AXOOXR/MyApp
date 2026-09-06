using Application.Projects.Commands.ActivateProject;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.DeactivateProject;
using Application.Projects.Commands.DeleteProject;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Queries.GetProjectById;
using Application.Projects.Queries.GetProjectHistory;
using Application.Projects.Queries.GetProjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ISender _sender;

    public ProjectsController(ISender sender)
    {
        _sender = sender;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<IActionResult> GetProjects(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetProjectsQuery(),
            cancellationToken);

        return Ok(result);
    }

    // GET: api/projects/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProject(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetProjectByIdQuery(id),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // GET: api/projects/5/history
    [HttpGet("{id:int}/history")]
    public async Task<IActionResult> GetHistory(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetProjectHistoryQuery(id),
            cancellationToken);

        return Ok(result);
    }

    // POST: api/projects
    [HttpPost]
    public async Task<IActionResult> CreateProject(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetProject),
            new { id },
            new { id });
    }

    // PUT: api/projects/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(
        int id,
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error); 

        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeleteProjectCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }

    // PATCH: api/projects/5/activate
    [HttpPatch("{id:int}/activate")]
    public async Task<IActionResult> ActivateProject(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ActivateProjectCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }

    // PATCH: api/projects/5/deactivate
    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> DeactivateProject(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeactivateProjectCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}