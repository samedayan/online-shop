using System.ComponentModel.DataAnnotations;
using Sendeo.OnlineShop.Order.Infrastructure.ValueObjects;
using Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain.Abstract;

namespace Sendeo.OnlineShop.Order.Persistence.PostgreSql.Domain;

public class OrderProduct : Entity
{
    public OrderProduct()
    {
        AuditInformation = new AuditInformation
        {
            CreatedDate = DateTime.Now.ToUniversalTime()
        };
    }
    
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}