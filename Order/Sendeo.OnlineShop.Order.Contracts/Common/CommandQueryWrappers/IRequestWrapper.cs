using MediatR;

namespace Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<out T> : IRequest<T>
	{
	}
}
