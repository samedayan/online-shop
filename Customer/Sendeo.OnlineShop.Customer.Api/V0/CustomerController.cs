using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sendeo.OnlineShop.Customer.Api.Filters.ValidationModels;
using Sendeo.OnlineShop.Customer.Contracts.User.Commands;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;
using Sendeo.OnlineShop.Customer.Contracts.User.Responses;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;
using Sendeo.OnlineShop.Customer.Domain.Extensions.Structures;
using Sendeo.OnlineShop.Customer.Infrastructure.Loggers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Sendeo.OnlineShop.Customer.Api.V0
{
	[Authorize]
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Route("api/v{version:apiVersion}/Customer")]
	public class CustomerController : ControllerBase
	{
		private readonly IConsoleLogger _logger;
		private readonly IMediator _mediator;

		public CustomerController(IConsoleLogger logger, IMediator mediator)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet("get-users")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(UserResponse))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetUsers([FromQuery] FindUserQuery request)
		{
			await _logger.LogInformation(request.AsJson());
	
			var response = await _mediator.Send(request);
		
			return Ok(response);
		}

		[HttpGet("get-user-by-id")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(UserViewModel))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> GetUserById(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new FindUserByIdQuery { Id = id});

			return Ok(response);
		}

		[HttpPost("create-user")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> CreateUser(CreateUserCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpPut("update-user")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> UpdateUser(UpdateUserCommand request)
		{
			await _logger.LogInformation(request.AsJson());

			var response = await _mediator.Send(request);

			return Ok(response);
		}

		[HttpDelete("delete-user")]
		[SwaggerResponse(StatusCodes.Status200OK, "Successfully response", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
		[SwaggerResponse(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> DeleteUser(int id)
		{
			await _logger.LogInformation(id.ToString());

			var response = await _mediator.Send(new DeleteUserCommand { Id = id});

			return Ok(response);
		}

	}
}