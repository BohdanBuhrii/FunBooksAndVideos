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
  }
}
