using MediatR;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public UpdateProductCommand(ProductViewModel product)
        {
            Product = product;
        }

        public ProductViewModel Product { get; set; }
    }
}
