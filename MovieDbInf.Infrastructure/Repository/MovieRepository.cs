using Microsoft.EntityFrameworkCore;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Domain.Repositories;
using MovieDbInf.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private DbSet<Movie> _dbSet;
        
        public MovieRepository(MovieDbInfContext movieDbContext) : base(movieDbContext)
        {
            _dbSet = movieDbContext.Set<Movie>();
        }

        public async Task<List<Movie>> GetByDirector(int id)
        {
           return await _dbSet.Where(x => x.DirectorId == id).ToListAsync();

        }
    }
}
