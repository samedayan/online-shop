using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sendeo.OnlineShop.Order.Contracts.Order.Commands;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;
using Sendeo.OnlineShop.Order.Contracts.Order.Responses;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;
using Sendeo.OnlineShop.Order.Domain.Extensions.Structures;
using Sendeo.OnlineShop.Order.Api.Filters.ValidationModels;
using Sendeo.OnlineShop.Order.Infrastructure.Loggers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Sendeo.OnlineShop.Product.Api.V0
{
    [ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Route("api/v{version:apiVersion}/Order")]
	public class OrderController : ControllerBase
	{
		private readonly IConsoleLogger _logger;
		private readonly IMediator _mediator;

		public OrderController(IConsoleLogger logger, IMediator mediator)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet("get-orders")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(OrderResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetOrders([FromQuery] GetOrderQuery request)
		{
			await _logger.LogInformation(request.AsJson());
	
			var response = await _mediator.Send(request);
		
			return Ok(response);
		}

		[HttpGet("get-order-by-id")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(OrderViewModel))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetOrderById(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new GetOrderByIdQuery { Id = id});

			return Ok(response);
		}

		[HttpPost("create-order")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> CreateOrder(CreateOrderCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpPost("create-orders")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> CreateOrders(List<CreateOrderCommand> request)
		{
			await _logger.LogInformation(request.AsJson());

			foreach (var item in request) 
			{
				await _mediator.Send(item);
			}

			return Ok(true);
		}

		[HttpPut("update-order")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> UpdateOrder(UpdateOrderCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpPut("update-orders")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> UpdateOrders(List<UpdateOrderCommand> request)
		{
			await _logger.LogInformation(request.AsJson());

			foreach (var item in request)
			{
				await _mediator.Send(item);
			}

			return Ok(true);
		}

		[HttpDelete("delete-order")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> DeleteOrder(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new DeleteOrderCommand { Id = id});

			return Ok(response);
		}
	}
}