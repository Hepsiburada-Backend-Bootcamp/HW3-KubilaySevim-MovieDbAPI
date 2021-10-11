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
        private readonly IDirectorRepository _directorRepository;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IDirectorRepository directorRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _directorRepository = directorRepository;
        }

        public async Task Add(MovieDto movieDto)
        {

            var result = _directorRepository.GetX(d => d.Last_name == movieDto.DirectorLastName);

            if (result.Result != null)
            {
                Movie movie = _mapper.Map<MovieDto,Movie>(movieDto);
                await _movieRepository.Add(movie);
            }

            throw new ApplicationException("Director Not Found");
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
