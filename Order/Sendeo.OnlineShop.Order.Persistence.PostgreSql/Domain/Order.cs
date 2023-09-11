using Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain
{
	public class Order : Entity
	{
		[Key]
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
	}
}
