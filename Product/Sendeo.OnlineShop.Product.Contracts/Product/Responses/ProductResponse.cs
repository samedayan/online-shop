using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Responses
{
    public class ProductResponse
    {
        public List<ProductViewModel> Data { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
