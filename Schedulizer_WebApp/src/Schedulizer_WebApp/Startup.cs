using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Schedulizer_WebApp.Services;
using Schedulizer_WebApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Schedulizer_WebApp.Controllers.Api;
using Schedulizer_WebApp.ViewModels;

namespace Schedulizer_WebApp
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .SetBasePath(appEnv.ApplicationBasePath)
            .AddJsonFile("config.json");
            Configuration = builder.Build();
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddLogging();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<CourseContext>();

            services.AddTransient<CourseContextSeedData>();
            services.AddScoped<ICourseRepository, CourseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, CourseContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Critical);

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                // trip to view and view to trip
                config.CreateMap<Course, CourseViewModel>().ReverseMap();
                config.CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsureSeedData();
        }

        // Entry point for the application. Runs once when web server starts.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
