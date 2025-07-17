using System;
using System.Collections.Generic;

namespace MyBlockForumServer.Database.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Nickname { get; set; }

    public string? Description { get; set; }

    public int? Karma { get; set; }

    public Guid StatusId { get; set; }

    public Guid RoleId { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Role Role { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();

    public virtual ICollection<User> FromUsers { get; set; } = new List<User>();

    public virtual ICollection<Thread> ThreadsNavigation { get; set; } = new List<Thread>();

    public virtual ICollection<User> ToUsers { get; set; } = new List<User>();
}
