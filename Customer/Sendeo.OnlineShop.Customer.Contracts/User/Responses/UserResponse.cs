using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Responses
{
	public class UserResponse
	{
		public List<UserViewModel> Data { get; set; } = new();
		public int TotalCount { get; set; }
	}
}
