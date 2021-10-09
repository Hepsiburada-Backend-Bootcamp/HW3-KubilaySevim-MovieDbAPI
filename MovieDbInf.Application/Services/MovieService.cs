using AutoMapper;
using MovieDbInf.Application.IServices;
using MovieDbInf.Application.Dto.Movie;
using MovieDbInf.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieDbInf.Domain.Entities;

namespace MovieDbInf.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public Task Add(MovieDto movieDto)
        {
            Movie movie = _mapper.Map<MovieDto,Movie>(movieDto);
            return _movieRepository.Add(movie);
        }

        public Task Delete(int id)
        {
            var director =    _movieRepository.Get(id);

            return _movieRepository.Delete(_mapper.Map<Domain.Entities.Movie>(director.Result));
        }

        public async Task<MovieDto> Get(int id)
        {
            
            var result = await _movieRepository.Get(id);

            return _mapper.Map<MovieDto>(result);
        }

        public async Task<List<MovieDto>> GetAll()
        {
            var result = await _movieRepository.GetAll();
            return  _mapper.Map<List<MovieDto>>(result);
        }

        public Task Update(int id, UpdateMovieDto movie)
        {
            throw new NotImplementedException();
        }
    }
}
