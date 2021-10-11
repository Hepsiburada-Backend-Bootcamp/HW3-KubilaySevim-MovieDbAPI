using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MovieDbInf.Application.Dto.Director;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Infrastructure.Context;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieDbInf.TestRepository.IntegrationTests
{
    public class DirectorControllerTest : IClassFixture<MovieDbInfApiFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;

        public DirectorControllerTest(MovieDbInfApiFactory factory)
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

        [Fact]
        public async Task Post_Add_Director_With_Same_FullName_Throws_Exception()
        {
            var director = new DirectorDto
            {
                First_name = "ffn", Last_name = "lfn", CountryId = 11, Id = 12 
            };

            var content = new StringContent(JsonSerializer.Serialize(director), Encoding.UTF8, "application/json");

            var responsePost = await _client.PostAsync("api/directors", content);

            //response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, responsePost.StatusCode);

            var responseGetAll = await _client.GetAsync("api/directors");
            var directorElement = await responseGetAll.Content.ReadAsStringAsync();
            var directorList = JsonSerializer.Deserialize<List<DirectorDto>>
            (JsonSerializer.Deserialize<JsonElement>(directorElement)
                .GetProperty("data").GetRawText(), new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            Assert.NotNull(directorList);
            //Assert.NotSame(director, directorList);

            var lastDirector = directorList.Last();
            Assert.Equal(director.First_name + director.Last_name, lastDirector.First_name + lastDirector.Last_name);

            var contentControlSameName =
                new StringContent(JsonSerializer.Serialize(director), Encoding.UTF8, "application/json");

            var exceptionSameName = await Assert.ThrowsAsync<ApplicationException>
                (async () => await _client.PostAsync("api/directors", contentControlSameName));
            
            Assert.Equal("Director name is exist", exceptionSameName.Message);
             
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