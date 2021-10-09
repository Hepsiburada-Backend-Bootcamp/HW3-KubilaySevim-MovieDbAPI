using MovieDbInf.Application.Dto.Director;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Application.IServices
{
    public interface IDirectorService
    {
        Task Add(DirectorDto director);

        Task Delete(int id);

        Task Update(int id, UpdateDirectorDto director);

        Task<List<DirectorDto>> GetAll();

        Task<DirectorDto> Get(int id);
    }
}
