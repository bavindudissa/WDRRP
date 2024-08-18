using System;

namespace WDRRP.Dtos;

public class PasswordChangeDto
{
    public string? Email { get; set;}
    public string? OldPassword { get; set;}
    public string? NewPassword { get; set;}
}
