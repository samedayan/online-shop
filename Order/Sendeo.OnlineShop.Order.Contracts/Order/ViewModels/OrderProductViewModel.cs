namespace Sendeo.OnlineShop.Order.Contracts.Order.ViewModels;

public class OrderProductViewModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}