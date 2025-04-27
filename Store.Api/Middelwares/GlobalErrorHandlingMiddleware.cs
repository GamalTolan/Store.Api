using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace Store.Api.Middelwares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
                if (httpcontext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    
                    await HandelNotFoundEndPointAsync(httpcontext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpcontext, ex);
            }
        }
        private async Task HandelNotFoundEndPointAsync(HttpContext httpcontext)
        {
            httpcontext.Response.ContentType = "application/json";

            var respons = new ErrorDetails
            {
                ErrorMessage =$"The End Point{httpcontext.Request.Path} NotFound!",
                StatusCode = (int)HttpStatusCode.NotFound
            }.ToString();
           

            await httpcontext.Response.WriteAsync(respons);

        }
        private async Task HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            //httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpcontext.Response.ContentType = "application/json";

            var respons = new ErrorDetails
            {
                ErrorMessage = ex.Message,
            };
            httpcontext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException validationException => HandelValidationException(validationException, respons),
                _ => (int)HttpStatusCode.InternalServerError
            };
            respons.StatusCode = httpcontext.Response.StatusCode;

            await httpcontext.Response.WriteAsync(respons.ToString());
        }
        private int HandelValidationException(ValidationException ex, ErrorDetails errorDetails)
        {
            errorDetails.Errors = ex.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
