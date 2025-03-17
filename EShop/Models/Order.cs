namespace EShop.Models
{
  public class Order
  {
    public int Id { get; set; }

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public required IEnumerable<Product> Products { get; set; }
  }
}
