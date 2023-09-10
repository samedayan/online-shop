﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Product.Domain.Installers;

namespace Sendeo.OnlineShop.Product.Application.Installers
{
	public static class ApplicationInstaller
	{
		public static void InstallApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.InstallDomain(configuration);
			serviceCollection.InstallMediatr();
		}
	}
}
