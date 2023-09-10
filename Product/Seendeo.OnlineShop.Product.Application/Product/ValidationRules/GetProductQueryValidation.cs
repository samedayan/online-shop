using FluentValidation;
using Sendeo.OnlineShop.Product.Contracts.Product.Queries;

namespace Sendeo.OnlineShop.Product.Application.Product.ValidationRules
{
    public class GetProductQueryValidation : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidation()
        {
            When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
            RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
        }
    }
}
