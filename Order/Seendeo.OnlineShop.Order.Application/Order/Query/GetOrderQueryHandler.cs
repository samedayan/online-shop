using Mapster;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Contracts.Order.Responses;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;
using Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;

namespace Sendeo.OnlineShop.Order.Application.Order.Query
{
    public class GetOrderQueryHandler : IRequestHandlerWrapper<GetOrderQuery, OrderResponse>
    {
        private readonly IOrderRepository _repository;

        public GetOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var (count, data) = _repository.GetOrders(request);

            var response = new OrderResponse
            {
                TotalCount = count
            };

            if (data != null && data.Any())
            {
                var map = data.Adapt<List<OrderViewModel>>();
                response.Data.AddRange(map);
            }

            return Task.FromResult(response);
        }
    }
}
