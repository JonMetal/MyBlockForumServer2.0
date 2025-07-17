using Mapster;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Models;
using MyBlockForumServer.Database.Repositories;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer.Database.Services
{
    public class ThreadService(IRepository<Thread> threadRepository,
        IRepository<ThreadTheme> threadThemeRepository) : IThreadService
    {
        readonly IRepository<Thread> _threadRepository = threadRepository;
        readonly IRepository<ThreadTheme> _threadThemeRepository = threadThemeRepository;

        public async Task<ThreadDto> CreateThreadAsync(ThreadDto thread)
        {
            Thread result = await _threadRepository.CreateAsync(thread.Adapt<Thread>());
            await _threadRepository.SaveAsync();
            return result.Adapt<ThreadDto>();
        }

        public async Task DeleteThreadAsync(Guid threadId)
        {
            await _threadRepository.DeleteAsync(threadId);
            await _threadRepository.SaveAsync();
        }

        public async Task UpdateThreadAsync(ThreadDto thread)
        {
            await _threadRepository.UpdateAsync(thread.Adapt<Thread>());
            await _threadRepository.SaveAsync();
        }

        public async Task<ThreadDto> GetThreadAsync(Guid id)
        {
            Thread? thread = await _threadRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(thread);
            return thread.Adapt<ThreadDto>();
        }

        public async Task<ThreadThemeDto> GetThreadThemeAsync(Guid id)
        {
            ThreadTheme? threadTheme = await _threadThemeRepository.GetAsync(id);
            ArgumentNullException.ThrowIfNull(threadTheme);
            return threadTheme.Adapt<ThreadThemeDto>();
        }
        public async Task<IEnumerable<ThreadDto>> GetAllThreadsAsync()
        {
            IEnumerable<Thread> threads = await _threadRepository.GetAllAsync();
            return threads.Select(t => t.Adapt<ThreadDto>());
        }

        public async Task<IEnumerable<ThreadDto>> GetThreadsByThemeAsync(Guid id)
        {
            IEnumerable<Thread> threads = await _threadRepository.GetAllAsync(l => l.ThreadThemeId == id);
            return threads.Select(t => t.Adapt<ThreadDto>());
        }

        public async Task<IEnumerable<ThreadThemeDto>> GetAllThreadThemesAsync()
        {
            IEnumerable<ThreadTheme> themes = await _threadThemeRepository.GetAllAsync();
            return themes.Select(t => t.Adapt<ThreadThemeDto>());
        }
    }
}
