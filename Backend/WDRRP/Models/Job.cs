using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Job
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Company { get; set; }

    public string? WorkplaceType { get; set; }

    public string? Location { get; set; }

    public string? JobType { get; set; }

    public string? Description { get; set; }

    public bool? ApplicantCollectEmail { get; set; }

    public int? UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
