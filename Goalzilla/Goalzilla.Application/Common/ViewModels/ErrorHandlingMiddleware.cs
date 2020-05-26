using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Goalzilla.Goalzilla.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Goalzilla.Goalzilla.Application.Common.ViewModels
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            HttpStatusCode code;
            string responseMessage = "";
            string logMessage;

            switch (exception)
            {
                case ValidationException ve:
                {
                    code = HttpStatusCode.BadRequest;
                    foreach ((var field, string[] errors) in ve.Errors)
                    {
                        var message = string.Join($", {Environment.NewLine}", errors);
                        responseMessage += $"{field}: {message}";
                    }
                    logMessage = responseMessage;
                    break;
                }
                default:
                {
                    code = HttpStatusCode.InternalServerError; 
                    responseMessage = $"{exception.Message}{Environment.NewLine}{exception.InnerException?.Message}";
                    logMessage = $"{responseMessage}{Environment.NewLine}{exception.StackTrace}";
                    break;
                }
            }

            logger.LogError(logMessage);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            var result = JsonConvert.SerializeObject(responseMessage);
            return context.Response.WriteAsync(result);
        }
    }
}