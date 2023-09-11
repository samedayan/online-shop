﻿using Sendeo.OnlineShop.Order.Api.Middlewares;

namespace Sendeo.OnlineShop.Order.Api.Extensions
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CorrelationIdMiddleware>();
		}

		public static IApplicationBuilder UseRequestResponseLogger(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
		}
	}
}
