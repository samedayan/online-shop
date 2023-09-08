using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Customer.Domain.Settings.Client;
using Sendeo.OnlineShop.Customer.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Customer.Domain.Settings.Validations
{
	public class ClientSettingsValidation : IValidateOptions<ClientSettings>
	{
		private readonly IConsoleLogger _logger;

		public ClientSettingsValidation(IConsoleLogger logger) 
		{
			_logger = logger;
		}

		public ValidateOptionsResult Validate(string name, ClientSettings options)
		{
			_logger.LogTrace($"{nameof(ClientSettings)}:{JsonSerializer.Serialize(options)}");

			if (!string.IsNullOrEmpty(options.Username?.Trim()))
				return ValidateOptionsResult.Success;

			if (!string.IsNullOrEmpty(options.Password?.Trim()))
				return ValidateOptionsResult.Success;

			_logger.LogError($"{options.GetType().Name}:{nameof(ClientSettings)} is null");
			return ValidateOptionsResult.Fail(
				$"{options.GetType().Name}:{nameof(ClientSettings)} is null");
		}
	}
}
