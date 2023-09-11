using MediatR;

namespace Sendeo.OnlineShop.Order.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<T> : IRequest<T>
	{
	}
}
