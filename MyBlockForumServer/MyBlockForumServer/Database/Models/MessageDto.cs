namespace MyBlockForumServer.Database.Models
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ThreadId { get; set; }

        public string? Text { get; set; }
    }
}
