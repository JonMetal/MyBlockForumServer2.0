using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class ThreadTheme
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
}
