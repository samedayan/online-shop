using Mapster;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;
using Sendeo.OnlineShop.Order.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Order.Infrastructure.ValueObjects;
using Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain;

namespace Sendeo.OnlineShop.Order.Consumer.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;

		public OrderService(IOrderRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> CreateOrderAsync(CreateOrderCommand command)
		{
			var order = command.Order.Adapt<Persistence.PostgreSql.Domain.Order>();
				
			order.AuditInformation = new AuditInformation
			{
				CreatedDate = DateTime.Now.ToUniversalTime(),
			};
			
			return await _repository.CreateOrderAsync(order);
		}

		public async Task<bool> UpdateOrderAsync(UpdateOrderCommand command)
		{
			var model = command.Order.Adapt<Persistence.PostgreSql.Domain.Order>();

			await _repository.UpdateOrderAsync(model);

			return true;
		}
	}
}
