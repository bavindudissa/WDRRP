using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class JobAlert
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? JobTitle { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
