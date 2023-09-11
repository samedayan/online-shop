using MediatR;

namespace Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<out T> : IRequest<T>
	{
	}
}
