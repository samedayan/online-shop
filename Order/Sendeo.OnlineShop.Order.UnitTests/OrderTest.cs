using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Sendeo.OnlineShop.Order.Application.Order.Query;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Domain.Repositories.Order;
using Sendeo.OnlineShop.Order.UnitTests.Mothers;

namespace Sendeo.OnlineShop.Product.UnitTests
{
	public class OrderTest
	{
		OrderEntityMother orderEntityMother = new OrderEntityMother();

		[Fact]
		public async Task GetAllProducts()
		{
			var mock = new Mock<IOrderRepository>();
			var mockLogger = new Mock<ILogger<GetOrderQuery>>().Object;
			var orderModel = orderEntityMother.GetOrder();

            IReadOnlyCollection<Order.Persistence.PostgreSql.Domain.Order> readOnlyUser = new List<Order.Persistence.PostgreSql.Domain.Order>
            {
				orderModel
			};

			mock.Setup(s => s.GetOrders(new GetOrderQuery { Page = 1, PageSize = 15})).Returns((1, readOnlyUser));

			var handler = new GetOrderQueryHandler(mock.Object);
			var result = await handler.Handle(new GetOrderQuery { Page = 1, PageSize = 15}, new CancellationToken());

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task CreateOrder()
		{
			var mock = new Mock<IOrderRepository>();
			var orderModel = orderEntityMother.GetOrder();

			mock.Setup(s => s.CreateOrderAsync(orderModel)).ReturnsAsync(true);

			var result = await mock.Object.CreateOrderAsync(orderModel);

			result.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateUser()
		{
			var mock = new Mock<IOrderRepository>();
			var orderModel = orderEntityMother.GetOrder();

			orderModel.CustomerId = 5;

			mock.Setup(s => s.UpdateOrderAsync(orderModel)).ReturnsAsync(true);

			var result = await mock.Object.UpdateOrderAsync(orderModel);

			result.Should().BeTrue();
		}
	}
}