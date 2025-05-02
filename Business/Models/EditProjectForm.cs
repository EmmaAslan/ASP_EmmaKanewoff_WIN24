using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class EditProjectForm
{
    public string Id { get; set; } = null!;

    [Display(Name = "Project Name", Prompt = "Enter project name")]
    [Required(ErrorMessage = "Required.")]
    [DataType(DataType.Text)]
    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters.")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required.")]
    [DataType(DataType.Text)]
    public string ClientName { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Enter project description")]
    [DataType(DataType.Text)]
    public string? Description { get; set; }

    [Display(Name = "Start Date", Prompt = "Enter start date")]
    [Required(ErrorMessage = "Required.")]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Display(Name = "End Date", Prompt = "Enter end date")]
    public DateOnly? EndDate { get; set; }

    [Display(Name = "Budget", Prompt = "Enter budget")]
    public decimal? Budget { get; set; }

    [Display(Name = "Status", Prompt = "Select project status")]
    public string? Status { get; set; }

    public int StatusId { get; set; }

}
