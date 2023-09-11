using FluentValidation;
using Sendeo.OnlineShop.Order.Contracts.Order.Queries;

namespace Sendeo.OnlineShop.Order.Application.Order.ValidationRules
{
    public class GetOrderQueryValidation : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidation()
        {
            When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
            RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
        }
    }
}
