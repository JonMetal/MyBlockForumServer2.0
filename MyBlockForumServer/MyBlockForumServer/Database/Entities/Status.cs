using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class Status
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
