using MassTransit;
using MediatR;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.Infrastructure.Constants;
using Sendeo.OnlineShop.Product.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Product.Application.Product.Commands
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _repository;
		private readonly IBus _bus;
		private readonly IConsoleLogger _consoleLogger;

		public CreateProductCommandHandler(IProductRepository repository, IBus bus, IConsoleLogger consoleLogger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_bus = bus ?? throw new ArgumentNullException(nameof(bus));
			_consoleLogger = consoleLogger ?? throw new ArgumentNullException(nameof(consoleLogger));
		}

		public async Task<bool> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
			var sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:{QueueNames.CreateProductHandlerQueueName}"));

			await sendEndpoint.Send(command, cancellationToken);
			await _consoleLogger.LogInformation($"Command Sent:{JsonSerializer.Serialize(command)}");

			return true;
        }
    }
}
