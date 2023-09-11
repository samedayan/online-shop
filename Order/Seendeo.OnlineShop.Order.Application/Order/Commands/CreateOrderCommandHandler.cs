using MassTransit;
using MediatR;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Infrastructure.Constants;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Order.Application.Order.Commands
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IBus _bus;
        private readonly IConsoleLogger _consoleLogger;

        public CreateOrderCommandHandler(IBus bus, IConsoleLogger consoleLogger)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _consoleLogger = consoleLogger ?? throw new ArgumentNullException(nameof(consoleLogger));
        }

        public async Task<bool> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var sendEndpoint = await _bus.GetSendEndpoint(new Uri($"queue:{QueueNames.CreateOrderHandlerQueueName}"));

            await sendEndpoint.Send(command, cancellationToken);
            await _consoleLogger.LogInformation($"Command Sent:{JsonSerializer.Serialize(command)}");

            return true;
        }
    }
}
