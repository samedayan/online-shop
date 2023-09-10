using Mapster;
using Sendeo.OnlineShop.Product.Contracts.Product.ViewModels;
using Sendeo.OnlineShop.Product.Infrastructure.Enums;
using Sendeo.OnlineShop.Product.Infrastructure.Extensions;

namespace Sendeo.OnlineShop.Product.Application.Product.Mapping
{
	public static class ProductObjectMapping
    {
        public static void Map()
        {
            TypeAdapterConfig<Persistence.PostgreSql.Domain.Product, ProductViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Category, src => EnumExtensions.GetEnumValueFromDescription<CategoryEnum>(src.CategoryId.ToString()))
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.ImagePath, src => src.ImagePath);
        }
    }
}
