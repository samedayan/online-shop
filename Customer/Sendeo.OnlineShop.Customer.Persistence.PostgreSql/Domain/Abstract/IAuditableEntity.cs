using Sendeo.OnlineShop.Customer.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain.Abstract
{
	public interface IAuditableEntity
	{
		public AuditInformation AuditInformation { get; set; }
	}
}
