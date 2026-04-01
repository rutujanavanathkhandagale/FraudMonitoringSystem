using System.Net;

namespace FraudMonitoringSystem.Exceptions.AlertCase
{
	public class GlobalExceptionMiddleware:Exception
	{

		private readonly RequestDelegate _next;


		public GlobalExceptionMiddleware(RequestDelegate next)

		{
			_next = next;

		}

		public async Task InvokeAsync(HttpContext context)

		{
			try
			{
				await _next(context);

			}
			catch (NotFoundException ex)

			{
				await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);

			}
			catch (ValidationException ex)

			{
				await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);

			}
			catch (AppException ex)

			{
				await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);

			}
			catch (System.Exception)

			{
				await HandleExceptionAsync(context, "Unexpected error occurred", HttpStatusCode.InternalServerError);

			}
		}

		private static Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)

		{

			context.Response.ContentType = "application/json";

			context.Response.StatusCode = (int)statusCode;

			return context.Response.WriteAsync($"{{\"error\":\"{message}\"}}");

		}
	}
}

