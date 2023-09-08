using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Sendeo.OnlineShop.Customer.Persistence.PostgreSql.Domain
{
	public class User : Entity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Phone { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public string Address { get; set; }
	}
}
