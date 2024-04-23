using System;
using System.Collections.Generic;

namespace WDRRP.Models;

public partial class ChatMessage
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? MessageType { get; set; }

    public byte[]? Document { get; set; }

    public string? DocumentUrl { get; set; }

    public int? SenderId { get; set; }

    public int? ChatId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
