using Sendeo.OnlineShop.Order.Contracts.Order.Commands;

namespace Sendeo.OnlineShop.Order.Consumer.Services
{
    public interface IOrderService
	{
		Task<bool> CreateOrderAsync(CreateOrderCommand command);
		Task<bool> UpdateOrderAsync(UpdateOrderCommand command);
	}
}
