using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        ViewBag.Statuses = new[] { "Not Started", "Started", "Completed" };

        return View(projects);
    }

    [HttpGet]
    [Route("api/getprojects/{id}")]
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
 }

