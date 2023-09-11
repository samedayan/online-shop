using Mapster;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.Infrastructure.Exceptions;
using Sendeo.OnlineShop.Product.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Product.Consumer.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _repository;

		public ProductService(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> CreateProductAsync(CreateProductCommand command)
		{
			var product = _repository.GetProducts(new GetProductQuery
			{
				Page = 1,
				PageSize = 10,
				Code = command.Product.Code
			});

			if (product.totalCount > 0 || product.Item2.Any())
			{
				throw new BusinessException("There is Product Code!.", ExceptionCodes.DefaultExceptionCode);
			}

			var model = command.Product.Adapt<Persistence.PostgreSql.Domain.Product>();

			model.AuditInformation = new AuditInformation
			{
				CreatedDate = DateTime.Now.ToUniversalTime(),
			};

			var isSaved = await _repository.CreateProductAsync(model);

			return isSaved;
		}

		public async Task<bool> UpdateProductAsync(UpdateProductCommand command)
		{
			var product = _repository.GetProductById(new GetProductByIdQuery { Id = command.Product.Id});

			if (product is null)
			{
				throw new BusinessException("Product Not Found!.", ExceptionCodes.DefaultExceptionCode);
			}

			var model = command.Product.Adapt<Persistence.PostgreSql.Domain.Product>();

			model.AuditInformation = new AuditInformation
			{
				CreatedDate = product.AuditInformation.CreatedDate,
				LastModifiedDate = DateTime.Now.ToUniversalTime()
			};

			return await _repository.UpdateProductAsync(model);
		}
	}
}
