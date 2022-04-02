using System;
using System.Linq;
using System.Linq.Expressions;
using Anima.Projeto.Domain.Shared.Entities;

namespace Anima.Projeto.Domain.Shared.Interfaces
{
    public interface IWriteRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : Entity;

        void Commit();

        void RemoveAsync<TEntity>(Guid id) where TEntity : Entity;

        IQueryable<TEntity> AsQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;

    }
}
