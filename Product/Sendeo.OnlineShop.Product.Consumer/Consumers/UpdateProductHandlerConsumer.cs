using MassTransit;
using Sendeo.OnlineShop.Product.Consumer.Services;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Infrastructure.Loggers;

namespace Sendeo.OnlineShop.Product.Consumer.Consumers
{
	public class UpdateProductHandlerConsumer : IConsumer<UpdateProductCommand>
	{
		private readonly IConsoleLogger _consumerLogger;
		private readonly IProductService _productService;

		public UpdateProductHandlerConsumer(IConsoleLogger consumerLogger, IProductService productService)
		{
			_consumerLogger = consumerLogger ?? throw new ArgumentNullException(nameof(consumerLogger));
			_productService = productService ?? throw new ArgumentNullException(nameof(productService));

		}

		public async Task Consume(ConsumeContext<UpdateProductCommand> context)
		{
			ArgumentNullException.ThrowIfNull(context.Message, nameof(context.Message));

			await _productService.UpdateProductAsync(context.Message);

			await _consumerLogger.LogInformation("Message Completed!.");
		}
	}
}
