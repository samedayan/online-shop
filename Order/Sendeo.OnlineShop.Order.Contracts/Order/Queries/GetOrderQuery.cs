using Sendeo.OnlineShop.Order.Contracts.Order.Responses;
using Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Queries
{
    public class GetOrderQuery : IRequestWrapper<OrderResponse>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
