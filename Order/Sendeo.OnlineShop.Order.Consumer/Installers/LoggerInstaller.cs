using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using Sendeo.OnlineShop.Order.Infrastructure.Extensions;

namespace Sendeo.OnlineShop.Order.Consumer.Installers
{
	public static class LoggerInstaller
	{
		public static void InstallLoggers(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var defaultLogLevel = configuration.GetSection("Logging:Consumer:LogLevel:Default").Value.ToEnum(LogLevel.Error);

			Console.Out.WriteLine($"Consumer:LogLevel:Default: {defaultLogLevel}");

			ConsoleLogger.DefaultLogLevel = defaultLogLevel;

			serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
		}
	}
}
