using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Sendeo.OnlineShop.Order.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using System.Net;

namespace Sendeo.OnlineShop.Order.Api.Extensions
{
	public static class ExceptionHandlerExtensions
	{
		public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
		{
			var consoleLogger = app.ApplicationServices.GetRequiredService<IConsoleLogger>();

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async handler =>
				{
					var ehf = handler.Features.Get<IExceptionHandlerFeature>();

					if (ehf?.Error != null)
					{
						var statusCode = HttpStatusCode.InternalServerError;
						string exceptionCode = ExceptionCodes.DefaultExceptionCode;

						switch (ehf.Error)
						{
							case KeyNotFoundException _:
								statusCode = HttpStatusCode.NotFound;
								break;
							case InvalidRequestException invalidRequestException:
								exceptionCode = invalidRequestException.ExceptionCode;
								statusCode = HttpStatusCode.BadRequest;
								break;
							case BusinessException businessException:
								exceptionCode = businessException.ExceptionCode;
								statusCode = HttpStatusCode.Conflict;
								break;
							case UnauthorizedException unauthorizedException:
								exceptionCode = unauthorizedException.ExceptionCode;
								statusCode = HttpStatusCode.Unauthorized;
								break;
						}

						await consoleLogger.LogError(ehf.Error.Message, ehf.Error);

						handler.Response.Clear();

						handler.Response.StatusCode = (int)statusCode;
						handler.Response.ContentType = @"application/json; charset=utf-8";

						var error = JsonConvert.SerializeObject(new
						{
							ErrorMessage = ehf.Error.Message,
							ExceptionCode = exceptionCode
						});

						await handler.Response.WriteAsync(error);
					}
				});
			});
		}
	}
}
