using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;

namespace Sendeo.OnlineShop.Customer.Domain.Repositories.User
{
	public interface IUserRepository
	{
		(int totalCount, IReadOnlyCollection<Persistence.PostgreSql.Domain.User>) FindUsers(FindUserQuery request);
		Persistence.PostgreSql.Domain.User? FindUserById(FindUserByIdQuery request);
		Task<bool> CreateUserAsync(Persistence.PostgreSql.Domain.User request);
		Task<bool> UpdateUserAsync(Persistence.PostgreSql.Domain.User request);
		Task<bool> DeleteUserAsync(DeleteUserCommand request);
	}
}
