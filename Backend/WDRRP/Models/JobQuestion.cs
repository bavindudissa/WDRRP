using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class JobQuestion
{
    public int Id { get; set; }

    public string? Question { get; set; }

    public string? Type { get; set; }

    public string? OptionOne { get; set; }

    public string? OptionTwo { get; set; }

    public string? Answer { get; set; }

    public int? JobId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
