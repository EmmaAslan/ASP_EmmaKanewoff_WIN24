using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService, IStatusService statusService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IStatusService _statusService = statusService;

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        ViewBag.Statuses = new[] { "Not Started", "Started", "Completed" };

        return View(projects);
    }

    [HttpGet]
    [Route("getprojects/{id}")]
    public async Task<IActionResult> GetProjectById(string id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound();

        var editForm = new EditProjectForm
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            ClientName = project.ClientName,
            Description = project.Description ?? "",
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            Status = project.Status,
            StatusId = project.StatusId
        };

        return Ok(editForm);
    }


    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });

        }

        var statusId = await _statusService.GetStatusIdByNameAsync(form.Status);
        if (statusId.HasValue)
        {
            form.StatusId = statusId.Value;
        }
        else
        {
            return BadRequest(new
            {
                success = false,
                errors = new Dictionary<string, string[]> {
            { "Status", new[] { "Selected status is not valid." } }
        }
            });
        }

        var result = await _projectService.AddProjectAsync(form);
        if (result)
        {
            return Ok(new { success = true });
        }
        else
        {
            return Problem("Unable to submit data.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditProject(EditProjectForm form, string id)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });

        }

        var result = await _projectService.UpdateProjectAsync(id, form);
        if (result)
        {
            return Ok(new { success = true });
        }
        else
        {
            return Problem("Unable to submit data.");
        }
    }

    [HttpDelete]
    [Route("deleteproject/{id}")]
    public async Task<IActionResult> DeleteProject(string id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        if (result)
        {
            return Ok(new { success = true });
        }
        else
        {
            return Problem("Unable to delete project.");
        }
    }
}

