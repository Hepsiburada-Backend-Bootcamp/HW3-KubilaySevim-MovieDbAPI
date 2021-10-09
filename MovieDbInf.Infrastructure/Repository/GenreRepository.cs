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
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private DbSet<Genre> _dbSet;

        public GenreRepository(MovieDbInfContext movieDbInfContext) : base(movieDbInfContext)
        {
            _dbSet = movieDbInfContext.Set<Genre>();
        }

        public async Task<List<Genre>> GetByMovies(int id)
        {
            return await _dbSet.Where(x => x.Id == id).ToListAsync();
        }
    }
}
