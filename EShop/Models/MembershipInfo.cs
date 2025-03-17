namespace EShop.Models
{
  public class MembershipInfo
  {
    public int MembershipId { get; set; }

    public required string Description { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }
  }
}
