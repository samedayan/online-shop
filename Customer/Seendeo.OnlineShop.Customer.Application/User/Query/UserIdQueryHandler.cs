using Mapster;
using Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;

namespace Sendeo.OnlineShop.Customer.Application.User.Query
{
	public class UserIdQueryHandler : IRequestHandlerWrapper<FindUserByIdQuery, UserViewModel>
	{
		private readonly IUserRepository _repository;

		public UserIdQueryHandler(IUserRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public Task<UserViewModel> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
		{
			var data = _repository.FindUserById(request);

			if (data is null)
			{
				return Task.FromResult(new UserViewModel());
			}

			var map = data.Adapt<UserViewModel>();

			return Task.FromResult(map);
		}
	}
}
