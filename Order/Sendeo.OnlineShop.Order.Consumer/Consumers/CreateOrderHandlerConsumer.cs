using MassTransit;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Consumer.Services;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;

namespace Sendeo.OnlineShop.Order.Consumer.Consumers
{
    public class CreateOrderHandlerConsumer : IConsumer<CreateOrderCommand>
	{
		private readonly IConsoleLogger _consumerLogger;
		private readonly IOrderService _orderService;

		public CreateOrderHandlerConsumer(IConsoleLogger consumerLogger, IOrderService orderService)
		{
			_consumerLogger = consumerLogger ?? throw new ArgumentNullException(nameof(consumerLogger));
			_orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

		}

		public async Task Consume(ConsumeContext<CreateOrderCommand> context)
		{
			ArgumentNullException.ThrowIfNull(context.Message, nameof(context.Message));

			await _orderService.CreateOrderAsync(context.Message);

			await _consumerLogger.LogInformation("Message Completed!.");
		}
	}
}
