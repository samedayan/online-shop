using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Application.Common.Behaviours;
using System.Reflection;

namespace Sendeo.OnlineShop.Order.Application.Installers
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
