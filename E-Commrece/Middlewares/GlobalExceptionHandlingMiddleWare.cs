using System.Net;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commrece.Middlewares
{
    public class GlobalExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleWare> _logger;
        
        public GlobalExceptionHandlingMiddleWare(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if(httpContext.Response.StatusCode==(int) HttpStatusCode.NotFound) 
                    await HandleNotFoundEndPointException(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Something Went Wrong{exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleNotFoundEndPointException(HttpContext httpContext)
        {
            httpContext.Response.ContentType= "application/json";
            var response =  new ErrorDetails
            {
                StatusCode=(int)HttpStatusCode.NotFound,
                ErrorMessage=$"The EndPoint{httpContext.Request.Path} is not Found.",
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        public async Task HandleExceptionAsync(HttpContext httpContext , Exception exception)
        {
            httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                
                ErrorMessage = exception.Message,
            };
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedExcptions => (int)HttpStatusCode.Unauthorized,
                RegisterValidationExcption validationExcption => handleValidationExcption(validationExcption, response),
                _ => (int)HttpStatusCode.InternalServerError
            };
            
            response.StatusCode=httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int handleValidationExcption(RegisterValidationExcption validationExcption, ErrorDetails response)
        {
            response.Errors=validationExcption.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
