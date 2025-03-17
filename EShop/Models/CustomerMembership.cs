namespace EShop.Models
{
  /// <summary>
  /// Customer-membership link.
  /// </summary>
  public class CustomerMembership
  {
    /// <summary>
    /// Customer identifier.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Membership identifier.
    /// </summary>
    public int MembershipId { get; set; }
    
    /// <summary>
    /// Membership expiration date.
    /// </summary>
    public DateTimeOffset ExpirationDate { get; set; }
  }
}
