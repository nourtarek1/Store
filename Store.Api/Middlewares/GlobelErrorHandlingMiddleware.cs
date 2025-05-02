using Domian.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErroresModels;

namespace Store.Api.Middlewares
{
    public class GlobelErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobelErrorHandlingMiddleware> _logger;

        public GlobelErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobelErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"End Point {context.Request.Path} is Not Found"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch(Exception ex)
            {
                // Log Exception 
                _logger.LogError(ex, ex.Message);

                // 1 . set status Code For Response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                // 2 . Set Content Typ 
                context.Response.ContentType = "application/json";
                // 3 . Ressponse (Body)
               var response =new ErrorDetails(){
                   //StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };

                response.StatusCode = ex switch
                {
                    NotFoundException =>StatusCodes.Status404NotFound,
                    BadRequestException =>StatusCodes.Status400BadRequest,
                    ValidationException => HandleValidationExceptionAsync((ValidationException)ex ,response),
                    UnAuthorizedException =>StatusCodes.Status401Unauthorized,
                    _ =>StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;

                await context.Response.WriteAsJsonAsync(response);
                // 4 .Return Response 

            }

            
        }
        private static int HandleValidationExceptionAsync(ValidationException ex , ErrorDetails response)
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
