using System;
using System.Linq;
using System.Linq.Expressions;
using Anima.Projeto.Domain.Shared.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using Anima.Projeto.Infrastructure.Data.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Repositories
{
    public class ReadRepository : IReadRepository
    {
        private AnimaContext _context;

        public ReadRepository(AnimaContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> AsQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {

            var query = _context.Set<TEntity>().AsNoTracking();
            if (includes != null)
                includes.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            return query;
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

        public IQueryable<TEntity> ApplyIncludesOnQuery<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : Entity
        {
            // Return Applied Includes query
            return (includeProperties.Aggregate(query, (current, include) => current.Include(include)));
        }

    }
}
