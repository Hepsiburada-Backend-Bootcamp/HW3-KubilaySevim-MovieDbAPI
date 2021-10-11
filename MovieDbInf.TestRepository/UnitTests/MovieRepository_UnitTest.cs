using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieDbInf.API.Controllers;
using MovieDbInf.Application.Dto.Movie;
using MovieDbInf.Application.IServices;
using MovieDbInf.Application.Services;
using MovieDbInf.Domain.Entities;
using Xunit;
using MovieDbInf.Domain.Repositories;
using MovieDbInf.Infrastructure.Repository;

namespace MovieDbInf.TestRepository.UnitTests
{
    public class MovieUnitTest
    {
        
        [Fact]
        public void GetAll_Return_MovieList()
        {
            //Arrange
            var movieRepoMock = new Mock<IMovieRepository>();
            var list = GetAllMovies();
            movieRepoMock.Setup(rep => rep.GetAll()).Returns(Task.FromResult(list));
            IMovieRepository movieRepository = movieRepoMock.Object;

            //Act
            var result = movieRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(list.Count, result.Result.Count);
        }

        [Fact]
        private void Delete_Movie_Element_From_List()
        {
            var mock = new Mock<IMovieRepository>();
            var list = GetAllMovies();
            var movieCount = list.Count;


            mock.Setup(repo => repo.Delete(It.IsAny<Movie>())).Returns((Movie m) =>
            {
                var movieItem = list.FirstOrDefault(li => li == m);
                list.Remove(movieItem);

                return Task.FromResult(movieCount > list.Count);
            });


            var movieRepository = mock.Object;

            var result = movieRepository.Delete(list[0]);

            Assert.NotEqual(GetAllMovies(), list);
        }
        
        //[Fact]
        public void Test_Add_Movie_Title_Requirement()
        {
            var movieDto = GetMovie();
            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(service => service.Add(movieDto)).Returns(Task.FromResult(true));
            IMovieService courseService = mockMovieService.Object;

            var result = courseService.Add(movieDto);
            var x = Task.FromResult(result);
            
            //Assert.True();
        }
        private List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();
            for (int i = 0; i < 5; i++)
            {
                Movie movie = new Movie();
                movie.Id = i;
                movie.Title = $"{i}.title";
                movie.ReleaseDate = i;

                movies.Add(movie);
            }

            return movies;
        }
        
        private MovieDto GetMovie()
        {
            MovieDto movie = new MovieDto();
            movie.Id = 2;
            movie.Title = $"{1}.title";
            movie.ReleaseDate = 1;
            return movie;
        }
    }
}