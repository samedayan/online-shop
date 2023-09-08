using Mapster;
using Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;
using Sendeo.OnlineShop.Customer.Contracts.User.Responses;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;

namespace Sendeo.OnlineShop.Customer.Application.User.Query
{
	public class UserQueryHandler : IRequestHandlerWrapper<FindUserQuery, UserResponse>
	{
		private readonly IUserRepository _repository;

		public UserQueryHandler(IUserRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public Task<UserResponse> Handle(FindUserQuery request, CancellationToken cancellationToken)
		{
			var (count, data) = _repository.FindUsers(request);

			var response = new UserResponse
			{
				TotalCount = count
			};

			if (data != null && data.Any())
			{
				var map = data.Adapt<List<UserViewModel>>();
				response.Data.AddRange(map);
			}

			return Task.FromResult(response);
		}
	}
}
