using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class Thread
{
    public Guid Id { get; set; }

    public Guid UserCreatorId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid ThreadThemeId { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ThreadTheme ThreadTheme { get; set; } = null!;

    public virtual User UserCreator { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
