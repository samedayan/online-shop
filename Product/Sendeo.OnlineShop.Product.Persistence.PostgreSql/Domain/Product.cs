using Sendeo.OnlineShop.Product.Persistence.PostgreSql.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Sendeo.OnlineShop.Product.Persistence.PostgreSql.Domain
{
	public class Product : Entity
	{
		[Key]
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }
		// TODO: Parse Price Stock
	}
}
