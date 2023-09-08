using FluentValidation;
using Sendeo.OnlineShop.Customer.Contracts.User.Queries;

namespace Sendeo.OnlineShop.Customer.Application.User.ValidationRules
{
	public class FindUserQueryValidation : AbstractValidator<FindUserQuery>
	{
		public FindUserQueryValidation()
		{
			When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
			RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
		}
	}
}
