using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Education
{
    public int Id { get; set; }

    public string? School { get; set; }

    public string? Degree { get; set; }

    public string? FieldOfStudy { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Grade { get; set; }

    public int? UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
