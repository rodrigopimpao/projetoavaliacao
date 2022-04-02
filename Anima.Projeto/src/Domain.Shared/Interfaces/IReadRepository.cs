using System;
using System.Linq;
using System.Linq.Expressions;
using Anima.Projeto.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Anima.Projeto.Domain.Shared.Interfaces
{
    public interface IReadRepository
    {
        IQueryable<TEntity> AsQueryable<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;
        IQueryable<TEntity> AsQueryableString<TEntity>(params string[] includes) where TEntity : Entity;

    }
}
