using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using Sendeo.OnlineShop.Order.Infrastructure.Extensions;

namespace Sendeo.OnlineShop.Order.Api.Installers
{
	public static class LoggerInstaller
	{
		public static void InstallLoggers(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var defaultLogLevel = configuration.GetSection("Logging:Console:LogLevel:Default").Value.ToEnum(LogLevel.Error);
			
			Console.Out.WriteLine($"Console:LogLevel:Default: {defaultLogLevel}");
			
			ConsoleLogger.DefaultLogLevel = defaultLogLevel;
			
			serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
		}
	}
}
