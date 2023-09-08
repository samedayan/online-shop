using MediatR;

namespace Sendeo.OnlineShop.Customer.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IRequestWrapper<TOut>
	{
	}
}
