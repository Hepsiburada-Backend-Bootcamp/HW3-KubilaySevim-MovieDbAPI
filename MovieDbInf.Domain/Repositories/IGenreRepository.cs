using MovieDbInf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Domain.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        public Task<List<Genre>> GetByMovies(int id);

    }
}
