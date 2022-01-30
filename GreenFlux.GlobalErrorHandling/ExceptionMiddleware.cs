using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.GlobalErrorHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException vEx)
            {
                await HandleValidationExceptionAsync(context, vEx, HttpStatusCode.BadRequest);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionWithDetailAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionWithDetailAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(ex.ToString());
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(ex.Message.ToString());
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ValidationErrorResponse()
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = ex.Message,
                status = (int)statusCode,
                errors = ex.Errors
            };

            var response = JsonSerializer.Serialize<ValidationErrorResponse>(errorResponse);
            await context.Response.WriteAsync(response);
        }
    }
}
