using MediatR;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Commands
{
	public class UpdateUserCommand : IRequest<bool>
	{
		public UpdateUserCommand(UserViewModel user)
		{
			User = user;
		}

		public UserViewModel User { get; set; }
	}
}
