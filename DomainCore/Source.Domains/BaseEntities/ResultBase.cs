using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace Source.Domains.BaseEntities
{
    public class ResultBase : ProblemDetails
    {
        public ResultBase(Exception ex, int status)
        {
            this.Exception = new ExceptionProblemDetails(ex, status);
            this.Code = status;
            this.Message = ex.Message ?? ex.InnerException.Message;
            this.Data = string.Empty;
        }
        public int? Code { get; set; }
        public string Message { get; set; }
        public string  Data { get; set; } = string.Empty;
        public ExceptionProblemDetails Exception { get; set; }
    }
}