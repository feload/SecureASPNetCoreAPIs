using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecureASPNetCoreAPIs.Models;

namespace SecureASPNetCoreAPIs.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }else{
                error.Message = "A server error ocurred.";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error) {
                StatusCode = 500
            };
        }
    }
}