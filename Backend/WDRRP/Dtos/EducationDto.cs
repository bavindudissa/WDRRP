namespace WDRRP.Dtos;

public class EducationDto
{
    public int? Id { get; set; }

    public string? School { get; set; }

    public string? Degree { get; set; }

    public string? FieldOfStudy { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Grade { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }
}
