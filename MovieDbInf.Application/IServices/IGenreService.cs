using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieDbInf.Application.Dto.Genre;

namespace MovieDbInf.Application.IServices
{
    public interface IGenreService
    {
        Task Add(GenreDto genre);

        Task Delete(int id);

        Task Update(int id, UpdateGenreDto genre);

        Task<List<GenreDto>> GetAll();

        Task<GenreDto> Get(int id);

    }
}
