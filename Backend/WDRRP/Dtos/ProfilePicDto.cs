using System;

namespace WDRRP.Dtos;

public class ProfilePicDto
{
    public int id { get; set; }

    public string? link { get; set; }

    public IFormFile? file { get; set; }
}
