using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Source.WebApi.Application.ActionResults;
using Source.WebApi.Application.Exceptions;

namespace Source.WebApi.Application.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
         private readonly IHostingEnvironment environment;
        //private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }

        public HttpGlobalExceptionFilter(IHostingEnvironment environment/*, ILogger<HttpGlobalExceptionFilter> logger*/)
        {
            this.environment = environment;
            //_logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            //logger.LogError(new EventId(context.Exception.HResult),
            //    context.Exception,
            //    context.Exception.Message);

            if (context.Exception.GetType() == typeof(DomainException))
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { context.Exception.Message }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if ((context.Exception.GetType() == typeof(AuthenticationException)) || (context.Exception.GetType() == typeof(AuthorizationException)))
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { context.Exception.Message }
                };

                context.Result = new UnauthorizedObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else 
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." }
                };

                if (environment.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }

    }
}