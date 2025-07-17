namespace MyBlockForumServer.Database.Models
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}
