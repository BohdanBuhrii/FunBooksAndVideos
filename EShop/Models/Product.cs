using EShop.Enumerations;

namespace EShop.Models
{
  public class Product
  {
    public int Id { get; set; }

    public string? Description { get; set; }

    public ProductType Type { get; set; }

    public decimal Price { get; set; }

    public bool IsPhysical { get; set; }

    public string? CoverUrl { get; set; }
  }
}
