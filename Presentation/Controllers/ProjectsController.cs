using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("admin/projects")]
public class ProjectsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
