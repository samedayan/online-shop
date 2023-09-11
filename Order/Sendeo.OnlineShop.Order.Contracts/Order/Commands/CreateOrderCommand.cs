using MediatR;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;
using Sendeo.OnlineShop.Order.Contracts.Events;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public CreateOrderCommand(OrderViewModel order)
        {
            Order = order;
        }

        public OrderViewModel Order { get; set; }
    }
}
