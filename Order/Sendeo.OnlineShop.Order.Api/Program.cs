using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sendeo.OnlineShop.Order.Api.Filters.Authentication;
using Sendeo.OnlineShop.Order.Api.Installers;
using Sendeo.OnlineShop.Order.Api.LifetimeHooks;
using Sendeo.OnlineShop.Order.Api.Middlewares;
using Sendeo.OnlineShop.Order.Application.Installers;
using Sendeo.OnlineShop.Order.Api.Extensions;

Console.WriteLine("Customer Api Starting!.");

var builder = WebApplication.CreateBuilder(args);

ConfigureHostSettings(builder.Host);

Console.WriteLine("Configured Host Customer Settings!.");

ConfigurationSettings(builder.Configuration);

RegisterServices(builder.Services, builder.Configuration);

Console.WriteLine("Services Customer Registered!.");

var app = builder.Build();

ConfigureWebApplication(app);

await app.RunAsync();

// Configure
#region Configure

void ConfigureHostSettings(IHostBuilder hostBuilder)
{
	// https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0
	// Wait 30 seconds for graceful shutdown.
	hostBuilder.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(45));
}

void ConfigurationSettings(IConfigurationBuilder configurationBuilder)
{
	configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
	configurationBuilder.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
	configurationBuilder.AddEnvironmentVariables();
}

void RegisterServices(IServiceCollection serviceCollection, IConfiguration configurationRoot)
{
	serviceCollection.InstallSettings(configurationRoot);

	serviceCollection.InstallLoggers(configurationRoot);

	serviceCollection.AddHealthChecks();

	serviceCollection.InstallControllers();

	serviceCollection.AddHostedService<ApplicationLifetimeService>();

	serviceCollection.InstallMassTransit(configurationRoot);

	serviceCollection.InstallSwagger();

	serviceCollection.InstallApplication(configurationRoot);

	serviceCollection.InstallMapper();

	serviceCollection.AddTransient<CorrelationIdMiddleware>();

	serviceCollection.AddTransient<RequestResponseLoggingMiddleware>();

	serviceCollection.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

}

void ConfigureWebApplication(IApplicationBuilder applicationBuilder)
{
	var provider = applicationBuilder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

	applicationBuilder.UseHttpsRedirection();
	applicationBuilder.UseRouting();
	applicationBuilder.UseAuthentication();
	applicationBuilder.UseAuthorization();
	applicationBuilder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
	applicationBuilder.InstallHealthCheck();
	applicationBuilder.UseGlobalExceptionHandler();
	applicationBuilder.UseCorrelationId();
	applicationBuilder.UseRequestResponseLogger();

	#region Swagger

	applicationBuilder.UseSwagger();

	applicationBuilder.UseSwaggerUI(options =>
	{
		// build a swagger endpoint for each discovered API version
		foreach (var description in provider.ApiVersionDescriptions)
			options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
				description.GroupName.ToUpperInvariant());

		// response list for export : https://github.com/swagger-api/swagger-ui/issues/3832#issuecomment-942470952
		options.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
		{
			["activated"] = false
		};
	});

	#endregion
}

#endregion