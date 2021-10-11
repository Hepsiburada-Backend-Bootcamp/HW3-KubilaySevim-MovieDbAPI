using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MovieDbInf.Application.Dto.Director;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Infrastructure.Context;
using Newtonsoft.Json;
using Xunit;

namespace MovieDbInf.TestRepository.IntegrationTests
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
       
   

        public async Task Post_Should_Return_Success_With_DirectorId_Response()
        {
            
        }

       
    }
}