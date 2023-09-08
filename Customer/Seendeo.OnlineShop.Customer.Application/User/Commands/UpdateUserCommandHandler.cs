using Mapster;
using MediatR;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;

namespace Sendeo.OnlineShop.Customer.Application.User.Commands
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
	{
		private readonly IUserRepository _repository;

		public UpdateUserCommandHandler(IUserRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var model = request.User.Adapt<Persistence.PostgreSql.Domain.User>();

			var isSaved = await _repository.UpdateUserAsync(model);

			return isSaved;
		}
	}
}
