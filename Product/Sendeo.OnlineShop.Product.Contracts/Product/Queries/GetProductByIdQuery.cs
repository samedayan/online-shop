using Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Queries
{
    public class GetProductByIdQuery : IRequestWrapper<ProductViewModel>
    {
        public int Id { get; set; }
    }
}
