using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieDbInf.Application;
using MovieDbInf.Domain.Entities;
using MovieDbInf.Infrastructure;
using MovieDbInf.Infrastructure.Context;

namespace MovieDbInf.TestRepository.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Test";
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieDbInfContext>();
            base.Configure(app, env);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
              
            services.AddApplicationModuleTestDb(Configuration);
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly); 

        }


    }
} 