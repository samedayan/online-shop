using Microsoft.AspNetCore.Http.Extensions;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Sendeo.OnlineShop.Order.Api.Middlewares
{
	public class RequestResponseLoggingMiddleware : IMiddleware
	{
		private readonly IConsoleLogger _consoleLogger;

		public RequestResponseLoggingMiddleware(IConsoleLogger consoleLogger)
		{
			_consoleLogger = consoleLogger;
		}

		private bool IsAppUrl(string requestUrl)
		{
			requestUrl = requestUrl.ToLower();
			return !requestUrl.Contains("/health") && !requestUrl.Contains(".ico") && !requestUrl.Contains("swagger");
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			string requestUrl = context.Request.GetDisplayUrl();

			if (!IsAppUrl(requestUrl))
				await next(context);

			if (!IsAppUrl(requestUrl))
				return;

			var method = context.Request.Method;
			string request = string.Empty;
			string response = string.Empty;

			if (ShouldApplyRequestLogging(method))
			{
				using (var bodyReader = new StreamReader(context.Request.Body))
				{
					request = await bodyReader.ReadToEndAsync();
					context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(request));
				}
			}

			context.Request.Headers.TryGetValue("Origin", out var origin);

			await _consoleLogger.LogInformation("API OnActionExecuting", null, response, request,
				new HttpMethod(method), HttpStatusCode.Processing, null, context.Request.Host.Value, requestUrl, origin);

			var timer = new Stopwatch();
			timer.Start();

			using (var buffer = new MemoryStream())
			{
				var stream = context.Response.Body;
				context.Response.Body = buffer;

				await next(context);
				timer.Stop();

				buffer.Seek(0, SeekOrigin.Begin);
				using (var bufferReader = new StreamReader(buffer))
				{
					response = await bufferReader.ReadToEndAsync();
					buffer.Seek(0, SeekOrigin.Begin);
					await buffer.CopyToAsync(stream);
				}
			}

			var durationInMilliseconds = timer.ElapsedMilliseconds;

			await _consoleLogger.LogInformation("API OnActionExecuted", null, response, request,
				new HttpMethod(method), (HttpStatusCode)context.Response.StatusCode, durationInMilliseconds,
				context.Request.Host.Value, requestUrl);
		}

		private bool ShouldApplyRequestLogging(string method)
		{
			if (method is "PUT" or "POST")
			{
				return true;
			}

			return false;
		}
	}
}
