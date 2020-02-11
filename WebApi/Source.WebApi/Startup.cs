using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using Domain.DataServices.Interfaces;
using Domain.DomainServices;
using Hellang.Middleware.ProblemDetails;
using Source.Core.DataService.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Source.DataServices.EFCore;
using Source.WebApi.Application.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Source.WebApi
{
    using Core.DataService;
    using Core.DomainService;
    using Source.DataServices.EFCore.DataContext;
    using EFCore.Setup;

    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _env.EnvironmentName = "Development";
            Configuration = configuration;

            DbContextDataInitializer.Initialize(new PostgressDbContext(Configuration));
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //generic services

            services.AddScoped<DbContext, PostgressDbContext>();

            services.AddTransient(typeof(IEntityDataService<>), typeof(EntityDataService<>));

            services.AddTransient(typeof(DomainService<,>));

            //custom services

            services.AddScoped<AppDbContext, PostgressDbContext>();

            services.AddTransient<ICaregiversDataService, CaregiversDataService>();

            services.AddTransient<CaregiversDomainService>();

            services.AddMvc(op =>
            {
                op.Filters.Add(typeof(HttpGlobalExceptionFilter));
                op.Filters.Add(typeof(HttpGlobalAuthorizationFilter));
                op.Filters.Add(typeof(HttpGlobalCultureFilter));

            });
                

            // Register the Swagger generator, defining 1 or more Swagger documents

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Source.WebApi",
                    Version = "v1.0",
                    Description = ""
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = _ => _env.IsDevelopment();
            });
            

            services.AddProblemDetails(
                ConfigureProblemDetails).AddMvcCore().AddJsonFormatters(x => x.NullValueHandling = NullValueHandling.Ignore);

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

            app.UseProblemDetails();

            app.UseMvc();
        }
        
        
        private void ConfigureProblemDetails(ProblemDetailsOptions options)
        {
            // This is the default behavior; only include exception details in a development environment.
            options.IncludeExceptionDetails = ctx => _env.IsDevelopment();
 
            // This will map NotImplementedException to the 501 Not Implemented status code.
            options.Map<NotImplementedException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status501NotImplemented));
 
            // This will map HttpRequestException to the 503 Service Unavailable status code.
            options.Map<HttpRequestException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status503ServiceUnavailable));
 
            // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            //options.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
 
            options.Map<SqlException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status409Conflict));
            //options.Map<EntityFramework.Exceptions>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status409Conflict));
 
            options.Map<AuthenticationException>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status401Unauthorized));
            options.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
 
        }
        
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            //Add header X-JWT-Assertion
            app.Use((context, next) =>
            {
                context.Request.Headers.Add("X-JWT-Assertion", "eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0=.eyJodHRwOlwvXC93c28yLm9yZ1wvY2xhaW1zXC9yb2xlIjpbIkludGVybmFsXC9jcmVhdG9yIiwiSW50ZXJuYWxcL3B1Ymxpc2hlciIsIkludGVybmFsXC9ldmVyeW9uZSIsIkFwcGxpY2F0aW9uXC90ZXN0X3Njb3BlMiIsIkFwcGxpY2F0aW9uXC90ZXN0X3Njb3BlMSJdLCJodHRwOlwvXC93c28yLm9yZ1wvY2xhaW1zXC9hcHBsaWNhdGlvbnRpZXIiOiJVbmxpbWl0ZWQiLCJodHRwOlwvXC93c28yLm9yZ1wvY2xhaW1zXC9rZXl0eXBlIjoiUFJPRFVDVElPTiIsImh0dHA6XC9cL3dzbzIub3JnXC9jbGFpbXNcL21vYmlsZSI6IjYyNjI5MDEwNCIsImh0dHA6XC9cL3dzbzIub3JnXC9jbGFpbXNcL3ZlcnNpb24iOiJ2MS4wLjAiLCJpc3MiOiJ3c28yLm9yZ1wvcHJvZHVjdHNcL2FtIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvYXBwbGljYXRpb25uYW1lIjoiRGVmYXVsdEFwcGxpY2F0aW9uIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvZW5kdXNlciI6IkdSVVBPQkMuTE9DQUxcL2F2YWxkZXNoQGdydXBvYmMuY29tQGNhcmJvbi5zdXBlciIsImh0dHA6XC9cL3dzbzIub3JnXC9jbGFpbXNcL2VuZHVzZXJUZW5hbnRJZCI6Ii0xMjM0IiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvZ2l2ZW5uYW1lIjoiQWxleGVpIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvZGlzcGxheU5hbWUiOiJBbGV4ZWkgVmFsZMOpcyBIdXJ0YWRvIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvZnVsbG5hbWUiOiJBbGV4ZWkgVmFsZMOpcyBIdXJ0YWRvIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvdGl0bGUiOiJEaXJlY3RvciBkZSBOdWV2YXMgVGVjbm9sb2fDrWFzIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvc3Vic2NyaWJlciI6ImFkbWluIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvdGllciI6IlVubGltaXRlZCIsImh0dHA6XC9cL3dzbzIub3JnXC9jbGFpbXNcL2VtYWlsYWRkcmVzcyI6ImF2YWxkZXNoQGdydXBvYmMuY29tIiwiaHR0cDpcL1wvd3NvMi5vcmdcL2NsYWltc1wvbGFzdG5hbWUiOiJWYWxkw6lzIEh1cnRhZG8iLCJodHRwOlwvXC93c28yLm9yZ1wvY2xhaW1zXC9hcHBsaWNhdGlvbmlkIjoiMTEiLCJodHRwOlwvXC93c28yLm9yZ1wvY2xhaW1zXC91c2VydHlwZSI6IkFQUExJQ0FUSU9OX1VTRVIiLCJleHAiOjE1MzcyNjM4MDQsImh0dHA6XC9cL3dzbzIub3JnXC9jbGFpbXNcL2FwaWNvbnRleHQiOiJcL2xvY2F0aW9uc1wvdjEuMC4wIn0=.");
                return next.Invoke();
            });

            //Call normal Configure
            Configure(app, _env);
        }

    }
}
