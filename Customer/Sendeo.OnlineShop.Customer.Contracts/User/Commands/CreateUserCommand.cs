using MediatR;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Commands
{
	public class CreateUserCommand : IRequest<bool>
	{
		public CreateUserCommand(UserViewModel user)
		{
			User = user;
		}

		public UserViewModel User { get; set; }
	}
}
