using Mapster;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;
using Sendeo.OnlineShop.Order.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Order.Infrastructure.ValueObjects;

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
			var model = command.Order.Adapt<Persistence.PostgreSql.Domain.Order>();

			model.AuditInformation = new AuditInformation
			{
				CreatedDate = DateTime.Now.ToUniversalTime(),
			};

			return await _repository.CreateOrderAsync(model);
		}

		public async Task<bool> UpdateOrderAsync(UpdateOrderCommand command)
		{
			var order = _repository.GetOrderById(new GetOrderByIdQuery { Id = command.Order.Id });

			if (order is null)
			{
				throw new BusinessException("Order Not Found!.", ExceptionCodes.DefaultExceptionCode);
			}

			var model = command.Order.Adapt<Persistence.PostgreSql.Domain.Order>();

			model.AuditInformation = new AuditInformation
			{
				CreatedDate = order.AuditInformation.CreatedDate,
				LastModifiedDate = DateTime.Now.ToUniversalTime()
			};

			return await _repository.UpdateOrderAsync(model);
		}
	}
}
