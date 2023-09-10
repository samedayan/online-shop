using MediatR;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.Infrastructure.Exceptions;

namespace Sendeo.OnlineShop.Product.Application.Product.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetProductById(new GetProductByIdQuery { Id = request.Id});

            if (product is null)
            {
                throw new BusinessException("Product Not Found!", ExceptionCodes.DefaultExceptionCode);
            }

            var isSaved = await _repository.DeleteProductAsync(product);

            return isSaved;
        }
    }
}
