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

namespace MovieDbInf.TestRepository
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
            Director director = new Director() { Id = 1,First_name = $"{1}.FN", Last_name = $"{1}.LN", CountryId = 1};
            var drRepoMock = new Mock<IDirectorRepository>();
            drRepoMock.Setup(repo => repo.Delete(It.IsAny<Director>()));
            drRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(GetAllDirectors()[0]);

            var directorRepository = drRepoMock.Object;

            Assert.ThrowsAsync<ApplicationException>(async () => await directorRepository.Delete(director));
        }
        
        // [Fact]
        // public void Update_Repo_MovieList()
        // {
        //     DirectorDto directorDto = new DirectorDto() {Id = 1, First_name = "aaa", Last_name = "ss"};
        //     var drRepoMock = new Mock<IDirectorRepository>();
        //     drRepoMock.Setup(repo => repo.Update(It.IsAny<Director>()));
        //     drRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(GetAllDirectors()[0]);
        //     IDirectorRepository directorRepository = drRepoMock.Object;
        //
        //     DirectorService directorService = new DirectorService(drRepoMock.Object);
        //     //Act
        //     var result = directorRepository.GetAll();
        //
        //     //Assert
        //     Assert.ThrowsAsync<ApplicationException>(async () => await directorService.Update(1,directorDto));
        // }
        //
        private List<Director> GetAllDirectors()
        {
            List<Director> movies = new List<Director>();
            for (int i = 0; i < 5; i++)
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

    }
}