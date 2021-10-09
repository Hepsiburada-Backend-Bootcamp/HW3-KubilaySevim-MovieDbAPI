using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MovieDbInf.Application.Dto.Director;
using MovieDbInf.Application.Dto.Movie;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Infrastructure.Context;
using MovieDbInf.TestRepository.IntegrationTests;
using Newtonsoft.Json;
using Xunit;

namespace MovieDbInf.TestRepository
{
    public class MovieControllerIT : IClassFixture<MovieDbInfApiFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;
        
         public MovieControllerIT(MovieDbInfApiFactory factory)
         {
             _factory = factory;
             _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
             {BaseAddress = new System.Uri("https://localhost")});
             
         }
       
        [Fact]
        public async Task Post_Should_Return_BadRequest_Without_Director_FirstName_Response_When_Insert_Success()
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;
 
            // Arrange
            var request = new DirectorDto()
            {
                 Id = 1,
                 Last_name = "ba",
                 CountryId = 1  
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
 
            // Act
            var response = await _client.PostAsync("api/directors", content);
            
            
            var actualStatusCode = response.StatusCode;

            Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        }

        public async Task Post_Should_Return_Success_With_DirectorId_Response()
        {
            
        }

        private void AddTestData(MovieDbInfContext context)
        {
            for (int j = 0; j < 5;j++)
            {
                Director director = new();
                director.Id = j + 1;
                director.First_name = $"FirstName {j}";
                director.Last_name = $"LastName {j}";
                director.Country.Id = j;
                    
                context.Directors.Add(director);
                context.SaveChanges();
            }
        }  
    }
}