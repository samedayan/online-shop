using Sendeo.OnlineShop.Product.Infrastructure.ValueObjects;

namespace Sendeo.OnlineShop.Product.Persistence.PostgreSql.Domain.Abstract
{
	public interface IAuditableEntity
	{
		public AuditInformation AuditInformation { get; set; }
	}
}
