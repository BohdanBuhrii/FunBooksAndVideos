namespace EShop.Models
{
  public class Membership
  {
    public int Id { get; set; }

    public required string Description { get; set; }

    public int DurationInDays { get; set; }
  }
}
