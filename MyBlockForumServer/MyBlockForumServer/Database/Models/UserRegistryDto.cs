namespace MyBlockForumServer.Database.Models
{
    public class UserRegistryDto
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
    }
}
