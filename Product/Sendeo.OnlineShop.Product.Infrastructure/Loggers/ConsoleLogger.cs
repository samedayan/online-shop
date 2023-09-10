using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sendeo.OnlineShop.Product.Infrastructure.Loggers
{
	public class ConsoleLogger : IConsoleLogger
	{
		public static LogLevel DefaultLogLevel { get; set; }

		public async Task Log(LogLevel logLevel, string message, Exception exception, string responseBody,
			string requestBody, HttpMethod httpMethod, HttpStatusCode httpStatusCode, long? duration, string hostName, string url, string origin)
		{
			if (DefaultLogLevel <= logLevel)
				await Console.Out.WriteLineAsync(JsonSerializer.Serialize(new
				{
					// TODO: CorrelationId
					DateTime = DateTime.UtcNow,
					LogLevel = logLevel.ToString(),
					Message = message,
					Exception = exception?.ToString(),
					ResponseBody = responseBody,
					RequestBody = requestBody,
					HttpMethod = httpMethod?.Method,
					HttpStatusCode = (int)httpStatusCode,
					Duration = duration,
					HostName = hostName,
					Url = url,
					Origin = origin
				}, new JsonSerializerOptions
				{
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
				}));
		}

		public async Task LogTrace(string message, Exception? exception = null, string? responseBody = null,
			string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string? hostName = null, string? url = null, string? origin = null)
		{
			await Log(LogLevel.Trace, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}

		public async Task LogDebug(string message, Exception? exception = null, string? responseBody = null,
			string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string? hostName = null, string? url = null, string? origin = null)
		{
			await Log(LogLevel.Debug, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}

		public async Task LogInformation(string message, Exception? exception = null, string? responseBody = null,
			string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string? hostName = null, string? url = null, string? origin = null)
		{
			await Log(LogLevel.Information, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}

		public async Task LogWarning(string message, Exception? exception = null, string? responseBody = null,
			string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string? hostName = null, string? url = null, string? origin = null)
		{
			await Log(LogLevel.Warning, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}

		public async Task LogError(string message, Exception exception, string responseBody,
			string requestBody, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string hostName = null, string url = null, string origin = null)
		{
			await Log(LogLevel.Error, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}

		public async Task LogCritical(string message, Exception exception, string responseBody,
			string requestBody,
			HttpMethod httpMethod, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string hostName = null,
			string url = null, string origin = null)
		{
			await Log(LogLevel.Critical, message, exception, responseBody, requestBody, httpMethod,
				httpStatusCode.GetValueOrDefault(), duration, hostName, url, origin);
		}
	}
}
