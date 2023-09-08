using Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Queries
{
	public class FindUserByIdQuery : IRequestWrapper<UserViewModel>
	{
		public int Id { get; set; }
	}
}
