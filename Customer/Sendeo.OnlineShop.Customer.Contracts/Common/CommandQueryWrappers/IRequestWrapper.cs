using MediatR;

namespace Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestWrapper<T> : IRequest<T>
	{
	}
}
