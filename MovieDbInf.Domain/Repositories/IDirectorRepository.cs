using MovieDbInf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Domain.Repositories
{
    public interface IDirectorRepository : IRepository<Director>
    {
        public Task<List<Director>> GetByCountry(int id);

    }
}
