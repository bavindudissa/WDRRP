using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class JobQuestionAnswer
{
    public int Id { get; set; }

    public int? JobQuestionId { get; set; }

    public string? Answer { get; set; }

    public int? JobAppliedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
