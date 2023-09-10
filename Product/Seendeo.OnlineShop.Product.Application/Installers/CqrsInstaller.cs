using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Product.Application.Common.Behaviours;
using System.Reflection;

namespace Sendeo.OnlineShop.Product.Application.Installers
{
	public static class CqrsInstaller
	{
		public static void InstallMediatr(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		}
	}
}
