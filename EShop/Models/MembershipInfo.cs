namespace EShop.Models
{
  /// <summary>
  /// Customer-specific membership info.
  /// </summary>
  public class MembershipInfo
  {
    /// <summary>
    /// Membership identifier.
    /// </summary>
    public int MembershipId { get; set; }

    /// <summary>
    /// Membership short description.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Expiration date of customer's membership.
    /// </summary>
    public DateTimeOffset ExpirationDate { get; set; }
  }
}
