using MediatR;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;
using Sendeo.OnlineShop.Order.Infrastructure.Exceptions;

namespace Sendeo.OnlineShop.Order.Application.Order.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetOrderById(new GetOrderByIdQuery { Id = request.Id });

            if (product is null)
            {
                throw new BusinessException("Product Not Found!", ExceptionCodes.DefaultExceptionCode);
            }

            var isSaved = await _repository.DeleteOrderAsync(product);

            return isSaved;
        }
    }
}
