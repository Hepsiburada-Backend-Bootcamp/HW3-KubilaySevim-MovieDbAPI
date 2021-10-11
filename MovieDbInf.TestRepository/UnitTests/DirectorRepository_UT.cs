using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MovieDbInf.Application.Dto.Director;
using MovieDbInf.Application.Dto.Movie;
using MovieDbInf.Application.IServices;
using MovieDbInf.Application.Services;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Domain.Repositories;
using Xunit;

namespace MovieDbInf.TestRepository.UnitTests
{
    public class DirectorService_UT
    {
        [Fact]
        public void GetAll_Return_MovieList()
        {
            //Arrange
            var drRepoMock = new Mock<IDirectorRepository>();
            var list = GetAllDirectors();
            drRepoMock.Setup(rep => rep.GetAll()).Returns(Task.FromResult(list));
            IDirectorRepository directorRepository = drRepoMock.Object;

            //Act
            var result = directorRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(list.Count, result.Result.Count);
        }
        
        [Fact]
        public void Delete_DirectorDto()
        {
            Director director = new Director() {};
            var list = GetAllDirectors();
            var drRepoMock = new Mock<IDirectorRepository>();
            drRepoMock.Setup(repo => repo.Delete(It.IsAny<Director>()));
            drRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(list[0]);

            var directorRepository = drRepoMock.Object;

            Assert.ThrowsAsync<ApplicationException>(async () => await directorRepository.Delete(director));
        }
        
        [Fact]
        public void Update_Repo_Director()
        {
            UpdateDirectorDto directorDto = new UpdateDirectorDto() {First_name = "aaa", Last_name = "ss"};
            var drRepoMock = new Mock<IDirectorService>();
            drRepoMock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateDirectorDto>()));
            drRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(getAllDirectorDtos()[0]);
            
            IDirectorService directorService = drRepoMock.Object;
        
            //Act
        
            //Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await directorService.Update(1, directorDto));
        }
        
        private List<Director> GetAllDirectors()
        {
            List<Director> movies = new List<Director>();
            for (int i = 1; i < 6; i++)
            {
                Director director = new Director();
                director.Id = i;
                director.First_name = $"{i}.FN";
                director.Last_name = $"{i}.LN";
                director.CountryId = i;
                movies.Add(director);
            }

            
            return movies;
        }
        private List<DirectorDto> getAllDirectorDtos()
        {
            List<DirectorDto> movies = new List<DirectorDto>();
            for (int i = 1; i < 6; i++)
            {
                DirectorDto director = new DirectorDto();
                director.Id = i;
                director.First_name = $"{i}.FN";
                director.Last_name = $"{i}.LN";
                director.CountryId = i;
                movies.Add(director);
            }

            
            return movies;
        }
    }
}