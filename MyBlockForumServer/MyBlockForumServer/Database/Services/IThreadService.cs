using MyBlockForumServer.Database.Models;

namespace MyBlockForumServer.Database.Services
{
    public interface IThreadService
    {
        Task<ThreadDto> CreateThreadAsync(ThreadDto thread);
        Task DeleteThreadAsync(Guid threadId);
        Task<IEnumerable<ThreadDto>> GetAllThreadsAsync();
        Task<IEnumerable<ThreadThemeDto>> GetAllThreadThemesAsync();
        Task<ThreadDto> GetThreadAsync(Guid id);
        Task<IEnumerable<ThreadDto>> GetThreadsByThemeAsync(Guid id);
        Task<ThreadThemeDto> GetThreadThemeAsync(Guid id);
        Task UpdateThreadAsync(ThreadDto thread);
    }
}