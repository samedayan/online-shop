using MediatR;
using Sendeo.OnlineShop.Product.Contracts.Events;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Commands
{
	public class CreateProductCommand : IRequest<bool>
    {
        public CreateProductCommand(ProductViewModel product)
        {
            Product = product;
        }

        public ProductViewModel Product { get; set; }
    }
}
