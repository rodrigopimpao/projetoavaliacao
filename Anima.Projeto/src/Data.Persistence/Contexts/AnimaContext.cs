using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Entities;
using Anima.Projeto.Infrastructure.Data.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Anima.Projeto.Infrastructure.Data.Persistence.Contexts
{
    public class AnimaContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        //public DbSet<Professor> Professors { get; set; }

        public DbSet<Avaliacao> Avaliacaos { get; set; }
        //public DbSet<Estudante> Estudantes { get; set; }

        public DbSet<Questao> Questaos { get; set; }

        public DbSet<UsuarioAvaliacao> UsuarioAvaliacaos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Nota> Notas { get; set; }

        public DbSet<Media> Medias { get; set; }

        public AnimaContext()
        {
            
        }

        public AnimaContext(DbContextOptions<AnimaContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        public AnimaContext(DbContextOptionsBuilder builder) : base(builder.Options)
        {
            Database.EnsureCreated();
        }

        public AnimaContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            //modelBuilder.ApplyConfiguration(new EstudanteConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new AvaliacaoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioAvaliacaoConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfessorAvaliacaoConfiguration());
            modelBuilder.ApplyConfiguration(new QuestaoConfiguration());
            modelBuilder.ApplyConfiguration(new AlternativaConfiguration());
            modelBuilder.ApplyConfiguration(new NotaConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new RespostaEstudanteConfiguration());
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            ChangeTracker
                .Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select(t => t.Entity as Entity)
                .AsParallel()
                .ForAll(entity => entity.UpdatedAt = DateTime.Now);
            return base.SaveChanges();
        }
    }
}
