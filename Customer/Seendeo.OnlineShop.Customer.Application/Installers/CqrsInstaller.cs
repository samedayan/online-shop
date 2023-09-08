using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Customer.Application.Common.Behaviours;
using System.Reflection;

namespace Sendeo.OnlineShop.Customer.Application.Installers
{
	public static class CqrsInstaller
	{
		public static void InstallMediatr(this IServiceCollection serviceCollection)
		{
			// serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
			serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		}
	}
}
