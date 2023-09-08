using Mapster;
using Sendeo.OnlineShop.Customer.Contracts.User.ViewModels;

namespace Sendeo.OnlineShop.Customer.Application.User.Mapping
{
	public static class UsergObjectMapping
	{
		public static void Map()
		{
			TypeAdapterConfig<Persistence.PostgreSql.Domain.User, UserViewModel>.NewConfig()
				.Map(dest => dest.Id, src => src.Id)
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.LastName, src => src.LastName)
				.Map(dest => dest.Phone, src => src.Phone)
				.Map(dest => dest.Email, src => src.Email)
				.Map(dest => dest.Address, src => src.Address);
		}
	}
}
