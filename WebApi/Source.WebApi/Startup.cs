﻿using Source.Core.DataService.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Source.WebApi
{
    using Core.DataService;
    using Core.DomainService;
    using Source.DataServices.EFCore;
    using Source.DataServices.EFCore.DataContext;
    using DataServices.Interfaces;
    using DomainServices;
    using EFCore.Setup;

    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;

            //var conex = Configuration.GetConnectionString("DefaultPostgresConnection");
            
            DbContextDataInitializer.Initialize(new PostgressDbContext(Configuration));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //generic services

            services.AddScoped<DbContext, PostgressDbContext>();

            services.AddTransient(typeof(IEntityDataService<>), typeof(EntityDataService<>));

            services.AddTransient(typeof(DomainService<,>));

            //custom services

            services.AddScoped<AppDbContext, PostgressDbContext>();

            services.AddTransient<IEmployeeDataService, EmployeeDataService>();

            services.AddTransient<EmployeeDomainService>();

            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Source.WebApi",
                    Version = "v1.0",
                    Description = ""
                });
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Source.WebApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
