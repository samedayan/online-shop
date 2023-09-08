using FluentAssertions;
using Mapster;
using Microsoft.Extensions.Logging;
using Moq;
using Sendeo.OnlineShop.Customer.Application.User.Commands;
using Sendeo.OnlineShop.Customer.Application.User.Query;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;
using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain;
using Sendeo.OnlineShop.Customer.UnitTests.Mothers;

namespace Sendeo.OnlineShop.Customer.UnitTests
{
	public class UserTest
	{
		UserEntityMother userEntityMother = new UserEntityMother();

		[Fact]
		public async Task GetAllUser()
		{
			var mock = new Mock<IUserRepository>();
			var mockLogger = new Mock<ILogger<FindUserQuery>>().Object;
			var userModel = userEntityMother.GetUser();

			IReadOnlyCollection<User> readOnlyUser = new List<User>
			{
				userModel
			};

			mock.Setup(s => s.FindUsers(new FindUserQuery { Page = 1, PageSize = 15})).Returns((1, readOnlyUser));

			var handler = new UserQueryHandler(mock.Object);
			var result = await handler.Handle(new FindUserQuery { Page = 1, PageSize = 15}, new CancellationToken());

			result.Should().NotBeNull();
		}

		[Fact]
		public async Task CreateUser()
		{
			var mock = new Mock<IUserRepository>();
			var userModel = userEntityMother.GetUser();

			mock.Setup(s => s.CreateUserAsync(userModel)).ReturnsAsync(true);

			var result = await mock.Object.CreateUserAsync(userModel);

			result.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateUser()
		{
			var mock = new Mock<IUserRepository>();
			var userModel = userEntityMother.GetUser();

			userModel.Name = "Test";
			userModel.LastName = "Test";

			mock.Setup(s => s.UpdateUserAsync(userModel)).ReturnsAsync(true);

			var result = await mock.Object.UpdateUserAsync(userModel);

			result.Should().BeTrue();
		}
	}
}