using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public class MessageRepository : AbstractRepository<Message>
    {
        public MessageRepository() : base() { }
        public override async Task<Message> CreateAsync(Message entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _db.Messages.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.Messages.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<Message?> GetAsync(Guid id)
        {
            return await _db.Messages.FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<Message?> GetAsync(Expression<Func<Message, bool>> predicate)
        {
            return await _db.Messages.FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _db.Messages.ToArrayAsync();
        }

        public override async Task<IEnumerable<Message>> GetAllAsync(Expression<Func<Message, bool>> predicate)
        {
            return await _db.Messages.Where(predicate).ToArrayAsync();
        }

        public override Task UpdateAsync(Message entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.Messages.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
