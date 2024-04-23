namespace WDRRP.Dtos;

public class ExperienceDto
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public int? EmploymentTypeId { get; set; }

    public string? CompanyName { get; set; }

    public string? Location { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public string? Industry { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }
}
