using uhlig.game.domain.Interfaces.Repositories;
using uhlig.game.domain.Entities;
using uhlig.game.infra.data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace uhlig.game.infra.data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly EmojiGameContext _context;
        public DbSet<T> Set { get; protected set; }
        public BaseRepository(EmojiGameContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            Set = context.Set<T>();
        }

        public void Insert(T entity)
        {
            Set.Add(entity);
            Commit();
        }
        public void Update(T entity)
        {
            Set.Update(entity);
            Commit();
        }
        public void Delete(T entity)
        {
            Set.Remove(entity);
            Commit();
        }
        public void Delete(Guid id)
        {
            var entity = this.Set.Find(id);

#nullable disable
            this.Delete(entity);
#nullable enable
            Commit();
        }
        public T? GetById(Guid id)
        {
            return Set.Find(id);
        }
        public bool Exists(Guid id) => Set.Any(x => x.Id == id);
        public IEnumerable<T>? GetAll()
        {
            return Set.AsEnumerable();
        }
        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> predicate)
        {
            return (IEnumerable<T>)Set.AsNoTracking().Where(predicate).ToList();
        }

        private void Commit() => _context.SaveChanges();

    }
}