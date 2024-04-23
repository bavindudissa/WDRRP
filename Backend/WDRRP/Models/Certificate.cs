using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Certificate
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Organization { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpireDate { get; set; }

    public string? CredentialId { get; set; }

    public string? CredentialUrl { get; set; }

    public int? UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
