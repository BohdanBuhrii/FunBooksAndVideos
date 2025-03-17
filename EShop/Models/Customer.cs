namespace EShop.Models
{
  /// <summary>
  /// Customer information.
  /// </summary>
  public class Customer
  {
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Full name of the customer.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Customer's preferred delivery address.
    /// </summary>
    public required string Address { get; set; }
  }
}
