using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("admin/projects")]
public class ProjectsController(IProjectService projectService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var projects = await projectService.GetAllProjectsAsync();

        return View(projects);
    }


}
