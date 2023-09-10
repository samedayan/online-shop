using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Product.Domain.Settings.Client;

namespace Sendeo.OnlineShop.Product.Domain.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IOptions<ClientSettings> _clientSettings;

		public AuthenticationService(IOptions<ClientSettings> clientSettings)
		{
			_clientSettings = clientSettings;
		}

		public bool ValidateCredentials(string username, string password) 
		{
			return username.Equals(_clientSettings.Value.Username) && password.Equals(_clientSettings.Value.Password);
		}
	}
}
