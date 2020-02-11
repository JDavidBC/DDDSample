using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Source.WebApi.Application.Filters
{
    public class HttpGlobalCultureFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Available cultures
            var availableCultures = new List<string>
            {
                "es-ES",
                //"en-US",
                //"cat-ES"
            };

            //Get "Culture" header param
            string headerCulture = context.HttpContext.Request.Headers["Culture"];
            //Remove "Culture" header
            context.HttpContext.Request.Headers.Remove("Culture");

            //Check culture value
            if (string.IsNullOrWhiteSpace(headerCulture) || !availableCultures.Contains(headerCulture))
            {
                headerCulture = "es-ES";
            }

            //Create culture object
            var culture = new CultureInfo(headerCulture);

            //Serializing Culture && add X-User-Identity to Request
            context.HttpContext.Request.Headers.Add("X-Culture", JsonConvert.SerializeObject(culture));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }

        
    }
}