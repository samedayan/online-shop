using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Order.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using System.Text.Json;

namespace Sendeo.OnlineShop.Order.Domain.Settings.Validations
{
	public class PostgresSettingsValidation : IValidateOptions<PostgresSettings>
	{
		private readonly IConsoleLogger _logger;

		public PostgresSettingsValidation(IConsoleLogger logger)
		{
			_logger = logger;
		}

		public ValidateOptionsResult Validate(string name, PostgresSettings options)
		{
			_logger.LogTrace($"{nameof(PostgresSettings)}:{JsonSerializer.Serialize(options)}");

			if (!string.IsNullOrEmpty(options.ConnectionString?.Trim()))
				return ValidateOptionsResult.Success;

			_logger.LogError($"{options.GetType().Name}:{nameof(options.ConnectionString)} is null");
			return ValidateOptionsResult.Fail(
				$"{options.GetType().Name}:{nameof(options.ConnectionString)} is null");
		}
	}
}
