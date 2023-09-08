using Mapster;
using MediatR;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;

namespace Sendeo.OnlineShop.Customer.Application.User.Commands
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
	{
		private readonly IUserRepository _repository;

		public CreateUserCommandHandler(IUserRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var model = request.User.Adapt<Persistence.PostgreSql.Domain.User>();

			model.AuditInformation = new Infrastructure.ValueObjects.AuditInformation();

			var isSaved = await _repository.CreateUserAsync(model);

			return isSaved;
		}
	}
}
