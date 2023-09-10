using Mapster;
using MediatR;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Product.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Product.Application.Product.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetProductById(new GetProductByIdQuery { Id = request.Product.Id });

            if (product is null)
            {
                throw new BusinessException("Product Not Found!", ExceptionCodes.DefaultExceptionCode); 
            }
            var model = request.Product.Adapt<Persistence.PostgreSql.Domain.Product>();

            model.AuditInformation = new AuditInformation
            {
                CreatedDate = product.AuditInformation.CreatedDate,
                LastModifiedDate = DateTime.Now.ToUniversalTime()
            };

            var isSaved = await _repository.UpdateProductAsync(model);

            return isSaved;
        }
    }
}
