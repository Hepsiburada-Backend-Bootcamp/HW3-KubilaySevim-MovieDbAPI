using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MovieDbInf.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MovieDbInfContext>
    {
        public MovieDbInfContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MovieDbInfContext>();
            var connectionString = "Server=localhost;Port=5432;Database=MovieDbInf;User Id=postgres;Password=12345;";
            builder.UseNpgsql(connectionString);
            return new MovieDbInfContext(builder.Options);
        }
    }
}
