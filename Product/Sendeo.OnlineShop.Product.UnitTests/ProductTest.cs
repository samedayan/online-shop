using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Sendeo.OnlineShop.Product.Application.Product.Query;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.UnitTests.Mothers;

namespace Sendeo.OnlineShop.Product.UnitTests
{
	public class ProductTest
	{
		ProductEntityMother productEntityMother = new ProductEntityMother();

		[Fact]
		public async Task GetAllProducts()
		{
			var mock = new Mock<IProductRepository>();
			var mockLogger = new Mock<ILogger<GetProductQuery>>().Object;
			var productModel = productEntityMother.GetProduct();

            IReadOnlyCollection<Persistence.PostgreSql.Domain.Product> readOnlyUser = new List<Persistence.PostgreSql.Domain.Product>
            {
				productModel
			};

			mock.Setup(s => s.GetProducts(new GetProductQuery { Page = 1, PageSize = 15})).Returns((1, readOnlyUser));

			var handler = new GetProductQueryHandler(mock.Object);
			var result = await handler.Handle(new GetProductQuery { Page = 1, PageSize = 15}, new CancellationToken());

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task CreateProduct()
		{
			var mock = new Mock<IProductRepository>();
			var userModel = productEntityMother.GetProduct();

			mock.Setup(s => s.CreateProductAsync(userModel)).ReturnsAsync(true);

			var result = await mock.Object.CreateProductAsync(userModel);

			result.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateUser()
		{
			var mock = new Mock<IProductRepository>();
			var userModel = productEntityMother.GetProduct();

			userModel.Name = "Test";
			userModel.Code = "Test";

			mock.Setup(s => s.UpdateProductAsync(userModel)).ReturnsAsync(true);

			var result = await mock.Object.UpdateProductAsync(userModel);

			result.Should().BeTrue();
		}
	}
}