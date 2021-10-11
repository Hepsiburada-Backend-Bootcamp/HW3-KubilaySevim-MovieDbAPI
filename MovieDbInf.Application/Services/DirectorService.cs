using AutoMapper;
using MovieDbInf.Application.Dto.Director;
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
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public DirectorService(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }


        public async Task Add(DirectorDto director)
        {
            var directorDto = (await _directorRepository.GetX(d =>
                d.First_name + d.Last_name == director.First_name + director.Last_name)).FirstOrDefault();

            if (directorDto != null) throw new ApplicationException("Director name is exist");
            await _directorRepository.Add(_mapper.Map<Domain.Entities.Director>(director));
        }


        public Task Update(int id, UpdateDirectorDto director)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DirectorDto>> GetAll()
        {
            //var dtoFilter =  _mapper.Map<Expression<Func<Domain.Entities.Director, bool>>>(filter);
            var result = (await _directorRepository.GetAll()).OrderBy(d => d.Id);
            return _mapper.Map<List<DirectorDto>>(result);
            ;
        }


        public async Task<DirectorDto> Get(int id)
        {
            var dre = await _directorRepository.Get(id);

            return _mapper.Map<DirectorDto>(dre);
        }

        public Task Delete(int id)
        {
            var director = _directorRepository.Get(id);

            return _directorRepository.Delete(_mapper.Map<Domain.Entities.Director>(director.Result));
        }
    }
}