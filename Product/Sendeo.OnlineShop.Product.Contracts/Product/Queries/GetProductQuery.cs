using Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Product.Contracts.Product.Responses;

namespace Sendeo.OnlineShop.Product.Contracts.Product.Queries
{
    public class GetProductQuery : IRequestWrapper<ProductResponse>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
