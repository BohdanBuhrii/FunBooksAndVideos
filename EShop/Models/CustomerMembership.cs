namespace EShop.Models
{
  public class CustomerMembership
  {
    public int CustomerId { get; set; }

    public int MembershipId { get; set; }
    
    public DateTimeOffset ExpirationDate { get; set; }
  }
}
