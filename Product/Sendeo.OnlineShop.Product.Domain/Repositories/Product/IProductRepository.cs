using Sendeo.OnlineShop.Product.Contracts.Product.Queries;

namespace Sendeo.OnlineShop.Product.Domain.Repositories.Product
{
	public interface IProductRepository
	{
		(int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.Product>) GetProducts(GetProductQuery request);
		Persistence.PostgreSql.Domain.Product? GetProductById(GetProductByIdQuery request);
		Task<bool> CreateProductAsync(Persistence.PostgreSql.Domain.Product request);
		Task<bool> UpdateProductAsync(Persistence.PostgreSql.Domain.Product request);
		Task<bool> DeleteProductAsync(Persistence.PostgreSql.Domain.Product product);
	}
}
