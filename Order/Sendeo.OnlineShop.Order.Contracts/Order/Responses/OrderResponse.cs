using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;

namespace Sendeo.OnlineShop.Order.Contracts.Order.Responses
{
    public class OrderResponse
    {
        public List<OrderViewModel> Data { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
