﻿using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Order.Domain.Settings.Client;

namespace Sendeo.OnlineShop.Order.Domain.Services
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
