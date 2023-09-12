using Sendeo.OnlineShop.Order.Contracts.Order.Queries;

namespace Sendeo.OnlineShop.Order.Domain.Repositories.Order
{
	public interface IOrderRepository
	{
		(int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.Order>) GetOrders(GetOrderQuery request);
		Persistence.PostgreSql.Domain.Order? GetOrderById(GetOrderByIdQuery request);
		Task<bool> CreateOrderAsync(Persistence.PostgreSql.Domain.Order request);
		Task<bool> UpdateOrderAsync(Persistence.PostgreSql.Domain.Order request);
		Task<bool> DeleteOrderAsync(Persistence.PostgreSql.Domain.Order order);
		Persistence.PostgreSql.Domain.OrderProduct? GetOrderProductById(int id);
		Task<bool> CreateOrderProductAsync(Persistence.PostgreSql.Domain.OrderProduct request);
		Task<bool> UpdateOrderProductAsync(Persistence.PostgreSql.Domain.OrderProduct request);


	}
}
