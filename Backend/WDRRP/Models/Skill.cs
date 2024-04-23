using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string? Skill1 { get; set; }

    public int? UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
