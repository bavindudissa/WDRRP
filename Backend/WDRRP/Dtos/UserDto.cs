namespace WDRRP.Dtos;

public class UserDto
{
    public int? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? ContactNumber { get; set; }

    public string? Password { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public int? UserTypeId { get; set; }

    public int? UserStatusId { get; set; }

    public bool? IsActive { get; set; }

    public string? ProfilePic { get; set; }
}
