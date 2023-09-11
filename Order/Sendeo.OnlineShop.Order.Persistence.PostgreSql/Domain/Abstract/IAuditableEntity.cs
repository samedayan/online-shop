using Sendeo.OnlineShop.Order.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain.Abstract
{
	public interface IAuditableEntity
	{
		public AuditInformation AuditInformation { get; set; }
	}
}
