using Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers;
using Sendeo.OnlineShop.Customer.Contracts.User.Responses;

namespace Sendeo.OnlineShop.Customer.Contracts.User.Queries
{
	public class FindUserQuery : IRequestWrapper<UserResponse>
	{
		public int? Page { get; set; }
		public int? PageSize { get; set; }
		public string? Email { get; set; }
		public string? Phone {  get; set; }
	}
}
