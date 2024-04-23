using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class JobFeedback
{
    public int Id { get; set; }

    public string? Feedback { get; set; }

    public string? Description { get; set; }

    public int? Rating { get; set; }

    public int? JobId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
