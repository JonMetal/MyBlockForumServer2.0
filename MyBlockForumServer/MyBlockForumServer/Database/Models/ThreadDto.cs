namespace MyBlockForumServer.Database.Models
{
    public class ThreadDto
    {
        public Guid Id { get; set; }

        public Guid UserCreatorId { get; set; }

        public Guid ThreadThemeId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}
