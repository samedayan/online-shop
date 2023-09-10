namespace Sendeo.OnlineShop.Product.Domain.Services
{
	public interface IAuthenticationService
	{
		bool ValidateCredentials(string username, string password);
	}
}
