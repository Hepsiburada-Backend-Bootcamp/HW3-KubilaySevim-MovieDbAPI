using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MovieDbInf.Infrastructure.Context;

namespace MovieDbInf.TestRepository.IntegrationTests
{
    public class MovieDbInfApiFactory : WebApplicationFactory<TestStartup>
    {


        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TestStartup>();
            });
        }
    }
}