using Sendeo.OnlineShop.Order.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Order.UnitTests.Mothers
{
	public class OrderEntityMother
	{
		public Persistence.PostgreSql.Domain.Order GetOrder()
		{
			return new Persistence.PostgreSql.Domain.Order
			{
				Id = new Random().Next(1, 9999),
				AuditInformation = new AuditInformation
				{
					CreatedDate = DateTime.Now.ToUniversalTime(),
					LastModifiedDate = DateTime.Now.ToUniversalTime(),
				},
				ProductId = 1,
				CustomerId = 2,
				Quantity = 3,
			};
		}
	}
}
