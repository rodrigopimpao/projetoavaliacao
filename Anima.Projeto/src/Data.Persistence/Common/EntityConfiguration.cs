using System;
using Anima.Projeto.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Common
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureKey(builder);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID");

            builder
                .Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("DT_CREATED");

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnName("DT_UPDATED");

            builder
                .Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("ST_ACTIVE");
        }

        public virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
