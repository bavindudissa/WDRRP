using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class JobApplied
{
    public int Id { get; set; }

    public int? JobId { get; set; }

    public int? UserId { get; set; }

    public byte[]? FileCv { get; set; }

    public string? Url { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
