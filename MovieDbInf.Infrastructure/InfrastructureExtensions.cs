using Microsoft.Extensions.DependencyInjection;
using MovieDbInf.Domain.Repositories;
using MovieDbInf.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieDbInf.Infrastructure.Context;
using System.Reflection;

namespace MovieDbInf.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();

            return services;
        }
        
        public static IServiceCollection AddInfrastructureModuleDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieDbInfContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("Default"),
                    b=>b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))
            );
            return services;
        }
        
        public static IServiceCollection AddInfrastructureModuleDbTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieDbInfContext>(
                options => options.UseInMemoryDatabase("MovieDbInf")
            );
            return services;
        }
    }
}
