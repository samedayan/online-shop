using MassTransit;
using Sendeo.OnlineShop.Product.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Product.Consumer.Observers
{
	public class ConsumeObserver : IConsumeObserver
	{
		private readonly IConsoleLogger _consumerLogger;

		public ConsumeObserver(IConsoleLogger consumerLogger)
		{
			_consumerLogger = consumerLogger;
		}

		public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
		{
			await _consumerLogger.LogInformation(
				$"TEventPre:{context.Message.GetType().Name}:{JsonSerializer.Serialize(context.Message)}");
			await Task.CompletedTask;
		}

		public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
		{
			await _consumerLogger.LogInformation(
				$"TEventPost:{context.Message.GetType().Name}:{JsonSerializer.Serialize(context.Message)}");
			await Task.CompletedTask;
		}

		public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
		{
			await _consumerLogger.LogError(
				$"TEventFault:{context.Message.GetType().Name}:{JsonSerializer.Serialize(context.Message)}",
				exception);
		}
	} 
}
