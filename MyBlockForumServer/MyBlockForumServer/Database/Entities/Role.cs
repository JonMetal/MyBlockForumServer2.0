using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
