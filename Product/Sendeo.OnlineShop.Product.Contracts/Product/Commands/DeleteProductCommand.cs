using MediatR;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
