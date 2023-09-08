using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Sendeo.OnlineShop.Customer.Api.Filters;
using System.Text.Json;
using FluentValidation.AspNetCore;
using Sendeo.OnlineShop.Customer.Application.Common.Behaviours;

namespace Sendeo.OnlineShop.Customer.Api.Installers
{
	public static class ControllerInstaller
	{
		public static void InstallControllers(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddControllers(setup =>
			{
				setup.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions()));
				setup.ReturnHttpNotAcceptable = true;

				setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
				setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
				setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
				setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));

				setup.Filters.Add<ValidationFilterAttribute>();
			}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly));

			serviceCollection.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
		}
	}
}
