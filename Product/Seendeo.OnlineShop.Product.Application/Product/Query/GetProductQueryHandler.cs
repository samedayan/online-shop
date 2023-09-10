using Mapster;
using Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Contracts.Product.Responses;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;

namespace Sendeo.OnlineShop.Product.Application.Product.Query
{
	public class GetProductQueryHandler : IRequestHandlerWrapper<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var (count, data) = _repository.GetProducts(request);

            var response = new ProductResponse
            {
                TotalCount = count
            };

            if (data != null && data.Any())
            {
                var map = data.Adapt<List<ProductViewModel>>();
                response.Data.AddRange(map);
            }

            return Task.FromResult(response);
        }
    }
}
