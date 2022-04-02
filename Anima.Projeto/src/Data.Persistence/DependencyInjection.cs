using System;
using Anima.Projeto.Domain.Shared.Interfaces;
using Anima.Projeto.Infrastructure.Data.Persistence.Contexts;
using Anima.Projeto.Infrastructure.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anima.Projeto.Infrastructure.Data.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("AnimaDb");

            //services.AddDbContext<AnimaContext>(options => options.UseSqlServer("Server=localhost;Database=DB_ANIMA;User Id=sa;Password=MyPass@word;"));

            services.AddDbContext<AnimaContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=projdb;Username=postgres;Password=postgres;"));

            services.AddScoped<IWriteRepository, WriteRepository>();
            services.AddScoped<IReadRepository, ReadRepository>();

            return services;
        }
    }
}
