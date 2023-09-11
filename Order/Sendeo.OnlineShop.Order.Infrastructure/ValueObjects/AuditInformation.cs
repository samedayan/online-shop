namespace Sendeo.OnlineShop.Order.Infrastructure.ValueObjects
{
	public record AuditInformation
	{
		public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
		public DateTime? LastModifiedDate { get; set; }
	}
}
