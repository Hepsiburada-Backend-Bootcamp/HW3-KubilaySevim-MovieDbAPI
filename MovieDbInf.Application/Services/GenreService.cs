using AutoMapper;
using MovieDbInf.Application.Dto.Genre;
using MovieDbInf.Application.IServices;
using MovieDbInf.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public Task Add(GenreDto genreDto)
        {
            return _genreRepository.Add(_mapper.Map<Domain.Entities.Genre>(genreDto));
        }


        public Task Delete(int id)
        {
            var genre =    _genreRepository.Get(id);

            return _genreRepository.Delete(_mapper.Map<Domain.Entities.Genre>(genre.Result));
        }

        public async Task<GenreDto> Get(int id)
        {
            
            var result = await _genreRepository.Get(id);

            return _mapper.Map<GenreDto>(result);
        }

        public async Task<List<GenreDto>> GetAll()
        {
            var result = await _genreRepository.GetAll();
            return _mapper.Map<List<GenreDto>>(result);
        }

        public Task Update(int id, UpdateGenreDto genre)
        {
            throw new NotImplementedException();
        }
    }
}
