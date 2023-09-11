using MediatR;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public UpdateOrderCommand(OrderViewModel order)
        {
            Order = order;
        }

        public OrderViewModel Order { get; set; }
    }
}
