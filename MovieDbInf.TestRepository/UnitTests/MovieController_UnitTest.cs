using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieDbInf.API.Controllers;
using MovieDbInf.Application.Dto.Movie;
using MovieDbInf.Application.IServices;
using MovieDbInf.Domain.Entities;
using Newtonsoft.Json;
using Xunit;

namespace MovieDbInf.TestRepository
{
    public class MovieControllerUnitTest
    {

        [Fact]
        public void GetMovieList_Returns_Movies()
        {
            var mockRepository = new Mock<IMovieService>();
            var mockLog = new Mock<ILogger<MovieController>>();
            var movieList = GetAllMovies();
            mockRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(movieList));

            var controller = new MovieController(mockRepository.Object, mockLog.Object);

            var result =controller.GetAll();
            
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsNotType<NotFoundResult>(result.Result);
            var returnedValue = result.Result as OkObjectResult;
            var actualData = returnedValue.Value as List<MovieDto>;
            Assert.Equal(movieList.Count, actualData.Count);
        }

        [Fact]
        public void AddMovie_Model_Not_Valid()
        {
      
            MovieDto movieDto = new MovieDto {  Id = 1,Title = "aa"};
            var mockRepository = new Mock<IMovieService>();
            var mockLog = new Mock<ILogger<MovieController>>();

            mockRepository.Setup(rep => rep.Add(It.IsAny<MovieDto>()));
            
            var controller = new MovieController(mockRepository.Object, mockLog.Object);
            var result =controller.Add(movieDto);


            var modelState = controller.ModelState;
            controller.ModelState.AddModelError("Title","minlength(3)");

            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        

        private List<MovieDto> GetAllMovies()
        {
            List<MovieDto> movies = new List<MovieDto>();
            for (int i = 0; i< 5; i++)
            {
                MovieDto movie = new MovieDto();
                movie.Id = i;
                movie.Title = $"{i}.title";
                movie.ReleaseDate = i;

                movies.Add(movie);
            }
            return movies;
        }
    }
}