using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Product.Application.Product.Mapping;

namespace Sendeo.OnlineShop.Product.Application.Installers
{
    public static class MapperInstaller
	{
		public static void InstallMapper(this IServiceCollection serviceCollection)
		{
			ProductObjectMapping.Map();
		}
	}
}
