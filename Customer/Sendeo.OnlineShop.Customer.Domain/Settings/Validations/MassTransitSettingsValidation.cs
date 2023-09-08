using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Customer.Domain.Settings.MassTransit;
using Sendeo.OnlineShop.Customer.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Customer.Domain.Settings.Validations
{
	public class MassTransitSettingsValidation : IValidateOptions<BusSettings>
	{
		private readonly IConsoleLogger _logger;

		public MassTransitSettingsValidation(IConsoleLogger logger)
		{
			_logger = logger;
		}

		public ValidateOptionsResult Validate(string name, BusSettings options)
		{
			_logger.LogTrace($"{nameof(BusSettings)}:{JsonSerializer.Serialize(options)}");

			if (string.IsNullOrEmpty(options.Password))
			{
				_logger.LogError($"{options.GetType().Name}:{nameof(options.Password)} is null");
				return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.Password)} is null");
			}

			if (string.IsNullOrEmpty(options.ClusterAddress))
			{
				_logger.LogError($"{options.GetType().Name}:{nameof(options.ClusterAddress)} is null");
				return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.ClusterAddress)} is null");
			}

			if (string.IsNullOrEmpty(options.UserName))
			{
				_logger.LogError($"{options.GetType().Name}:{nameof(options.UserName)} is null");
				return ValidateOptionsResult.Fail($"{options.GetType().Name}:{nameof(options.UserName)} is null");
			}

			return ValidateOptionsResult.Success;
		}
	}
}
