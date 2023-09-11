namespace Sendeo.OnlineShop.Order.Contracts.Order.ViewModels
{
	public class OrderViewModel
    {
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
	}
}
