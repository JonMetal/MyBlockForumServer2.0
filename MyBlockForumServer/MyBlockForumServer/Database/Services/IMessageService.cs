using MyBlockForumServer.Database.Models;

namespace MyBlockForumServer.Database.Services
{
    public interface IMessageService
    {
        Task DeleteMessageAsync(Guid id);
        Task EditMessageAsync(MessageDto message);
        Task<IEnumerable<MessageDto>> GetAllMessagesByUserAsync(Guid userId);
        Task<IEnumerable<MessageDto>> GetAllMessagesFromThreadAsync(Guid threadId);
        Task<MessageDto> GetMessageAsync(Guid id);
        Task<MessageDto> SendMessageAsync(MessageDto message);
    }
}