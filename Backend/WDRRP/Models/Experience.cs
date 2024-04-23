using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Experience
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? EmploymentTypeId { get; set; }

    public string? CompanyName { get; set; }

    public string? Location { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public string? Industry { get; set; }

    public int? UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
