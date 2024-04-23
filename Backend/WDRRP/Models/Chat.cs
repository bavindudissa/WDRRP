using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int? SeekerId { get; set; }

    public int? InterviewerId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
