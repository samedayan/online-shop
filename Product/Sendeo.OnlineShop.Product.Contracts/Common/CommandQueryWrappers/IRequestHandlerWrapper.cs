using MediatR;

namespace Sendeo.OnlineShop.Product.Contracts.Common.CommandQueryWrappers
{
	public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IRequestWrapper<TOut>
	{
	}
}
