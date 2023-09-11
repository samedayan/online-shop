using MediatR;

namespace Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<out T> : IRequest<T>
	{
	}
}
