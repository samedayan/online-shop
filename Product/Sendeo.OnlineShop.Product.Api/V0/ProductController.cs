using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sendeo.OnlineShop.Product.Api.Filters.ValidationModels;
using Sendeo.OnlineShop.Product.Contracts.Product.Commands;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;
using Sendeo.OnlineShop.Product.Contracts.Product.Responses;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;
using Sendeo.OnlineShop.Product.Domain.Extensions.Structures;
using Sendeo.OnlineShop.Product.Infrastructure.Loggers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Sendeo.OnlineShop.Product.Api.V0
{
    [ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Route("api/v{version:apiVersion}/Product")]
	public class ProductController : ControllerBase
	{
		private readonly IConsoleLogger _logger;
		private readonly IMediator _mediator;

		public ProductController(IConsoleLogger logger, IMediator mediator)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet("get-products")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(ProductResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetProducts([FromQuery] GetProductQuery request)
		{
			await _logger.LogInformation(request.AsJson());
	
			var response = await _mediator.Send(request);
		
			return Ok(response);
		}

		[HttpGet("get-product-by-id")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(ProductViewModel))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetProductById(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new GetProductByIdQuery { Id = id});

			return Ok(response);
		}

		[HttpPost("create-product")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> CreateProduct(CreateProductCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpPost("create-products")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> CreateProducts(List<CreateProductCommand> request)
		{
			await _logger.LogInformation(request.AsJson());

			foreach (var item in request) 
			{
				await _mediator.Send(item);
			}

			return Ok(true);
		}

		[HttpPut("update-product")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> UpdateProduct(UpdateProductCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpPut("update-products")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> UpdateProducts(List<UpdateProductCommand> request)
		{
			await _logger.LogInformation(request.AsJson());

			foreach (var item in request)
			{
				await _mediator.Send(item);
			}

			return Ok(true);
		}

		[HttpDelete("delete-product")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> DeleteProduct(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new DeleteProductCommand { Id = id});

			return Ok(response);
		}
	}
}