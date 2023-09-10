using Mapster;
using Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;

namespace Sendeo.OnlineShop.Product.Application.Product.Query
{
	public class GetProductByIdQueryHandler : IRequestHandlerWrapper<GetProductByIdQuery, ProductViewModel>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetProductById(request);

            if (data is null)
            {
                return Task.FromResult(new ProductViewModel());
            }

            var map = data.Adapt<ProductViewModel>();

            return Task.FromResult(map);
        }
    }
}
