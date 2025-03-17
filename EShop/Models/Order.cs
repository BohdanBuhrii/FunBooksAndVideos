namespace EShop.Models
{
  /// <summary>
  /// Order information.
  /// </summary>
  public class Order
  {
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Total price to be paid.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Customer identifier.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// List of products.
    /// </summary>
    public required IEnumerable<Product> Products { get; set; }
  }
}
