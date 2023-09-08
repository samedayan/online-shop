using System.Diagnostics;

namespace Sendeo.OnlineShop.Customer.Api.Middlewares
{
	public class CorrelationIdMiddleware : IMiddleware
	{
		private const string CorrelationIdKey = "X-Correlation-ID";
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			Trace.CorrelationManager.ActivityId = context.Request.Headers.TryGetValue(CorrelationIdKey, out var correlationId) ? new Guid(correlationId) : Guid.NewGuid();

			await next(context);
		}
	}
}
