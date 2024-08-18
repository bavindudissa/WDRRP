using System;

namespace WDRRP.Dtos;

public class ForgotPasswordDto
{
    public string? Email { get; set; }
    public string? ResetUrl { get; set; }
}
