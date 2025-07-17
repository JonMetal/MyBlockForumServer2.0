using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer.Database.Repositories
{
    public class ThreadRepository : AbstractRepository<Thread>
    {
        public override async Task<Thread> CreateAsync(Thread entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ThreadTheme? threadTheme = await _db.ThreadThemes.FirstOrDefaultAsync(t => t.Id == entity.ThreadThemeId);
            ArgumentNullException.ThrowIfNull(threadTheme, $"ThreadTheme with Id {entity.ThreadThemeId} not found.");
            entity.ThreadTheme = threadTheme;
            _db.Entry(entity.ThreadTheme).State = EntityState.Unchanged;
            await _db.Threads.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.Threads.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<Thread?> GetAsync(Guid id)
        {
            return await _db.Threads.Include(l => l.Users).Include(l => l.ThreadTheme).FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<Thread?> GetAsync(Expression<Func<Thread, bool>> predicate)
        {
            return await _db.Threads.FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Thread>> GetAllAsync()
        {
            return await _db.Threads.Include(l => l.ThreadTheme).ToArrayAsync();
        }

        public override async Task<IEnumerable<Thread>> GetAllAsync(Expression<Func<Thread, bool>> predicate)
        {
            return await _db.Threads.Include(l => l.ThreadTheme).Where(predicate).ToArrayAsync();
        }

        public override Task UpdateAsync(Thread entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.Threads.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
