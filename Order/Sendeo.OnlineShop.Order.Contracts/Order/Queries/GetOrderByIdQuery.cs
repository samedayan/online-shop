using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;
using Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Queries
{
    public class GetOrderByIdQuery : IRequestWrapper<OrderViewModel>
    {
        public int Id { get; set; }
    }
}
