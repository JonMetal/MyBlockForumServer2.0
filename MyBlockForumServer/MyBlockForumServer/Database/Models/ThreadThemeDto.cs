namespace MyBlockForumServer.Database.Models
{
    public class ThreadThemeDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}
