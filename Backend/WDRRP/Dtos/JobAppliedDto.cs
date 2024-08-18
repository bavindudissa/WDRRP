using System;

namespace WDRRP.Dtos;

public class JobAppliedDto
{
    public int id { get; set; }

    public int? jobId { get; set; }

    public int? userId { get; set; }

    public byte[]? fileCv { get; set; }

    public string? url { get; set; }

    public string? status { get; set; }

    public bool isActive { get; set; }

    public float? matchPercentage { get; set; }

    public IFormFile? file { get; set; }

    public JobDto? jobDto{ get; set; }

    public UserDto? userDto{ get; set; }
}
