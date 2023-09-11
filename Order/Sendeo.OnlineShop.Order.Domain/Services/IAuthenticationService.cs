namespace Sendeo.OnlineShop.Order.Domain.Services
{
	public interface IAuthenticationService
	{
		bool ValidateCredentials(string username, string password);
	}
}
