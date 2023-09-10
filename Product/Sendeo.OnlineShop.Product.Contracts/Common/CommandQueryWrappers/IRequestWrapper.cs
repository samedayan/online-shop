using MediatR;

namespace Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<T> : IRequest<T>
	{
	}
}
