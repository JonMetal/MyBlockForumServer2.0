using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public class ThreadThemeRepository : AbstractRepository<ThreadTheme>
    {
        public override async Task<ThreadTheme> CreateAsync(ThreadTheme entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _db.ThreadThemes.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.ThreadThemes.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<ThreadTheme?> GetAsync(Guid id)
        {
            return await _db.ThreadThemes.FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<ThreadTheme?> GetAsync(Expression<Func<ThreadTheme, bool>> predicate)
        {
            return await _db.ThreadThemes.FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<ThreadTheme>> GetAllAsync()
        {
            return await _db.ThreadThemes.ToArrayAsync();
        }

        public override async Task<IEnumerable<ThreadTheme>> GetAllAsync(Expression<Func<ThreadTheme, bool>> predicate)
        {
            return await _db.ThreadThemes.Where(predicate).ToArrayAsync();
        }

        public override Task UpdateAsync(ThreadTheme entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.ThreadThemes.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
