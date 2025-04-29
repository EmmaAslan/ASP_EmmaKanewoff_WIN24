namespace Business.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public decimal? Budget { get; set; }
    public string Status { get; set; } = null!;
    public int StatusId { get; set; }
}
