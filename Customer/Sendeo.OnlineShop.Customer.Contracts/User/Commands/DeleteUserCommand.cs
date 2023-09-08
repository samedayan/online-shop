using MediatR;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Commands
{
	public class DeleteUserCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}
}
