using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FootballManager.Application.Infrastructure.AutoMapper;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using FootballManager.Application.Infrastructure;
using FootballManager.Application;
using FootballManager.Persistence;
using FootballManager.Api.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using FootballManager.Persistence.Repositories;
using FootballManager.Persistence.Repositories.PlayerRepository;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using Microsoft.Data.Sqlite;

namespace FootballManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FootballManager", Version = "v1" });
            });

            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
                   options.DefaultApiVersion = new ApiVersion(0, 1, "Active");
                   options.Conventions.Controller<PlayerController>().HasApiVersion(0, 1);
                   options.Conventions.Controller<TeamController>().HasApiVersion(0, 1);
                   options.Conventions.Controller<StadiumController>().HasApiVersion(0, 1);
               });

            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            var connectionString = "DataSource=myshareddb;mode=memory;cache=shared";
            var keepAliveConnection = new SqliteConnection(connectionString);
            keepAliveConnection.Open();

            services.AddDbContext<FootballManagerDbContext>(options => options.UseSqlite(connectionString));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IStadiumRepository, StadiumRepository>();

            // Add MediatR
            services.AddMediatR(typeof(RequestHandlerBase).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FootballManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
