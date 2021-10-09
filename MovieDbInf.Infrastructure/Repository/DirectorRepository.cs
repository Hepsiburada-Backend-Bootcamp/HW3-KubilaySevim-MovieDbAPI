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
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        private DbSet<Director> _dbSet;
        private readonly MovieDbInfContext _movieDbInfContext;


        public DirectorRepository(MovieDbInfContext movieDbInfContext) : base(movieDbInfContext)
        {
            _movieDbInfContext = movieDbInfContext;
            _dbSet = _movieDbInfContext.Set<Director>();
        }

        public async Task<List<Director>> GetByCountry(int id)
        {
            return await _dbSet.Where(x => x.Id == id).ToListAsync();

        }


    }
}
