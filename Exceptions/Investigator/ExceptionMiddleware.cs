using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FraudMonitoringSystem.Exceptions.Investigator
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next) => _next = next;


        public async Task InvokeAsync(HttpContext httpContext)

        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)

            {
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)

        {
            HttpStatusCode status;

            string message = exception.Message;


            if (exception is NotFoundException)

                status = HttpStatusCode.NotFound;

            else if (exception is BadRequestException)

                status = HttpStatusCode.BadRequest;

            else
            {
                status = HttpStatusCode.InternalServerError;

                message = "An unexpected error occurred.";

            }

            var response = new { error = message };

            var payload = JsonSerializer.Serialize(response);


            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)status;


            return context.Response.WriteAsync(payload);

        }

    }
}
