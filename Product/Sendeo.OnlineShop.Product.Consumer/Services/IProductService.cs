using Sendeo.OnlineShop.Product.Contracts.Product.Commands;

namespace Sendeo.OnlineShop.Product.Consumer.Services
{
	public interface IProductService
	{
		Task<bool> CreateProductAsync(CreateProductCommand command);
		Task<bool> UpdateProductAsync(UpdateProductCommand command);
	}
}
