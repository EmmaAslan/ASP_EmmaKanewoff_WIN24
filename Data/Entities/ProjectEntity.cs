using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateOnly StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? EndDate { get; set; }
    public decimal? Budget { get; set; }


    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public virtual StatusEntity Status { get; set; } = null!;
}
