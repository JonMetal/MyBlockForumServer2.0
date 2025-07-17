using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ThreadId { get; set; }

    public string? Text { get; set; }

    public virtual Thread Thread { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
