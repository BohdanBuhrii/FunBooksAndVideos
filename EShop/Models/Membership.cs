namespace EShop.Models
{
  /// <summary>
  /// Membership information.
  /// </summary>
  public class Membership
  {
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Membership short description.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Duration in days.
    /// </summary>
    public int DurationInDays { get; set; }
  }
}
