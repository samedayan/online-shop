using MediatR;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
