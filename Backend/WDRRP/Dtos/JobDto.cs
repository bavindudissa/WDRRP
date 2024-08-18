namespace WDRRP;

public class JobDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Company { get; set; }

    public string? WorkplaceType { get; set; }

    public string? Location { get; set; }

    public string? JobType { get; set; }

    public string? Description { get; set; }

    public string? ApplicantCollectEmail { get; set; }

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public bool IsActive { get; set; }
}
