using Microsoft.Extensions.Logging;
using System.Net;

namespace Sendeo.OnlineShop.Order.Infrastructure.Loggers
{
	public interface IConsoleLogger
	{
		Task Log(LogLevel logLevel, string message, Exception exception, string responseBody, string requestBody,
			HttpMethod httpMethod, HttpStatusCode httpStatusCode, long? duration, string hostName, string url,
			string origin);

		Task LogTrace(string message, Exception exception = null, string responseBody = null,
			string requestBody = null, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null,
			long? duration = null, string hostName = null, string url = null, string origin = null);

		Task LogDebug(string message, Exception exception = null, string responseBody = null, string requestBody = null,
			HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string hostName = null, string url = null, string origin = null);

		Task LogInformation(string message, Exception exception = null, string responseBody = null,
			string requestBody = null, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null,
			long? duration = null, string hostName = null, string url = null,
			string origin = null);

		Task LogWarning(string message, Exception exception = null, string responseBody = null,
			string requestBody = null, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null,
			long? duration = null, string hostName = null, string url = null,
			string origin = null);

		Task LogError(string message, Exception exception = null, string responseBody = null, string requestBody = null,
			HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
			string hostName = null, string url = null, string origin = null);

		Task LogCritical(string message, Exception exception = null, string responseBody = null,
			string requestBody = null, HttpMethod httpMethod = null, HttpStatusCode? httpStatusCode = null,
			long? duration = null, string hostName = null, string url = null, string origin = null);
	}
}
