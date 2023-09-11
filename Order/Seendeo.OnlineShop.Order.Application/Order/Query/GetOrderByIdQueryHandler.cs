using Mapster;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;
using Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;

namespace Sendeo.OnlineShop.Order.Application.Order.Query
{
    public class GetOrderByIdQueryHandler : IRequestHandlerWrapper<GetOrderByIdQuery, OrderViewModel>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<OrderViewModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetOrderById(request);

            if (data is null)
            {
                return Task.FromResult(new OrderViewModel());
            }

            var map = data.Adapt<OrderViewModel>();

            return Task.FromResult(map);
        }
    }
}
