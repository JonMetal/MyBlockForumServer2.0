using Mapster;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Repositories;

namespace MyBlockForumServer.Database.Services
{
    public class MessageService(IRepository<Message> messageRepository) : IMessageService
    {
        readonly IRepository<Message> _messageRepository = messageRepository;

        public async Task<MessageDto> SendMessageAsync(MessageDto message)
        {
            ArgumentNullException.ThrowIfNull(message);
            Message result = await _messageRepository.CreateAsync(message.Adapt<Message>());
            await _messageRepository.SaveAsync();
            return result.Adapt<MessageDto>();
        }

        public async Task EditMessageAsync(MessageDto message)
        {
            await _messageRepository.UpdateAsync(message.Adapt<Message>());
            await _messageRepository.SaveAsync();
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            await _messageRepository.DeleteAsync(id);
            await _messageRepository.SaveAsync();
        }

        public async Task<MessageDto> GetMessageAsync(Guid id)
        {
            Message? message = await _messageRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(message);
            MessageDto dto = message.Adapt<MessageDto>();
            return dto;
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesByUserAsync(Guid userId)
        {
            IEnumerable<Message> messages = await _messageRepository.GetAllAsync(l => l.UserId == userId);
            return messages.Select(l => l.Adapt<MessageDto>());
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesFromThreadAsync(Guid threadId)
        {
            IEnumerable<Message> messages = await _messageRepository.GetAllAsync(l => l.ThreadId == threadId);
            return messages.Select(l => l.Adapt<MessageDto>());
        }
    }
}
