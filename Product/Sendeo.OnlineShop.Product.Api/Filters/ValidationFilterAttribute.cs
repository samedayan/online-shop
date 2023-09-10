using Microsoft.AspNetCore.Mvc.Filters;
using Sendeo.OnlineShop.Product.Api.Filters.ValidationModels;

namespace Sendeo.OnlineShop.Product.Api.Filters
{
	public class ValidationFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
		}
	}
}
