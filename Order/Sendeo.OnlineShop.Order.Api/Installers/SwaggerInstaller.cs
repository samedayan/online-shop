using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sendeo.OnlineShop.Order.Api.Settings.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Sendeo.OnlineShop.Order.Api.Installers
{
	public static class SwaggerInstaller
	{
		public static void InstallSwagger(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VV";
				options.SubstituteApiVersionInUrl = true;
			});

			serviceCollection.AddApiVersioning(options =>
			{
				options.Conventions.Add(new VersionByNamespaceConvention());
				options.DefaultApiVersion = new ApiVersion(0, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});

			serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			serviceCollection.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sendeo Order Api", Version = "v1" });
				options.AddSecurityDefinition("basic", new OpenApiSecurityScheme 
				{
					Name = "Authorization", 
					Type = SecuritySchemeType.Http, 
					Scheme = "basic",
					In = ParameterLocation.Header, 
					Description = "Basic Authorization header using the Bearer scheme." 
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement 
				{ 
					{ 
						new OpenApiSecurityScheme 
						{ 
							Reference = new OpenApiReference 
							{ 
								Type = ReferenceType.SecurityScheme, 
								Id = "basic" } }, new string[] { }
					} 
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				options.IncludeXmlComments(xmlPath);
				options.EnableAnnotations();
				options.OperationFilter<SwaggerDefaultValues>();
				options.CustomSchemaIds(x => x.ToString());
			});
		}
	}
}
