namespace Sendeo.OnlineShop.Customer.Domain.Services
{
	public interface IAuthenticationService
	{
		bool ValidateCredentials(string username, string password);
	}
}
