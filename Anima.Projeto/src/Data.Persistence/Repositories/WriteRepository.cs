using System;
using System.Linq;
using System.Linq.Expressions;
using Anima.Projeto.Domain.Shared.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using Anima.Projeto.Infrastructure.Data.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Repositories
{
    public class WriteRepository : IWriteRepository
    {
        private AnimaContext _context;

        public WriteRepository(AnimaContext context)
        {
            _context = context;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IQueryable<TEntity> AsQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryableString<TEntity>(params string[] includes) where TEntity : Entity
        {

            var query = _context.Set<TEntity>().AsNoTracking().AsQueryable();
            if (includes != null)
                includes.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            return query;
        }

        public void Remove<TEntity>(Guid id) where TEntity : Entity
        {
            var table = _context.Set<TEntity>();

            var entity = table.Find(id);

            table.Remove(entity);

        }
    }
}
