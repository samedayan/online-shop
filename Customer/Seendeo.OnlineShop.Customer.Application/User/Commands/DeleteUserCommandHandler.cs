using MediatR;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;

namespace Sendeo.OnlineShop.Customer.Application.User.Commands
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
	{
		private readonly IUserRepository _repository;

		public DeleteUserCommandHandler(IUserRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{	
			var isSaved = await _repository.DeleteUserAsync(request);

			return isSaved;
		}
	}
}
