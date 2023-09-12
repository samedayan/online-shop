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
                .Map(dest => dest.StatusId, src => src.StatusId)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreatedDate, src => src.AuditInformation.CreatedDate)
                .Map(dest => dest.LastModifiedDate, src => src.AuditInformation.LastModifiedDate);
            
            TypeAdapterConfig<Persistence.PostgreSql.Domain.OrderProduct, OrderProductViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.OrderId, src => src.OrderId)
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.Quantity, src => src.Quantity)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreatedDate, src => src.AuditInformation.CreatedDate)
                .Map(dest => dest.LastModifiedDate, src => src.AuditInformation.LastModifiedDate);
        }
    }
}
