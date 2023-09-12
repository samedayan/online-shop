namespace Sendeo.OnlineShop.Order.Contracts.Order.ViewModels
{
	public class OrderViewModel
    {
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int StatusId { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? LastModifiedDate { get; set; }
		public IList<OrderProductViewModel> OrderProducts { get; set; }
	}
}
