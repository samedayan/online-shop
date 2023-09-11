using Mapster;
using Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;

namespace Sendeo.OnlineShop.Order.Application.Order.Mapping
{
	public static class OrderObjectMapping
    {
        public static void Map()
        {
            TypeAdapterConfig<Persistence.PostgreSql.Domain.Order, OrderViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ProductId, src => src.ProductId);
        }
    }
}
