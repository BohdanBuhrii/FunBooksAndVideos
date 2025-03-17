using EShop.Enumerations;

namespace EShop.Models
{
  /// <summary>
  /// Product info.
  /// </summary>
  public class Product
  {
    /// <summary>
    /// Identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product's description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Type of the product.
    /// </summary>
    public ProductType Type { get; set; }

    /// <summary>
    /// Price per item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Indicates whether product is eligible for physical shipping.
    /// </summary>
    public bool IsPhysical { get; set; }

    /// <summary>
    /// Url to the cover image for the product.
    /// </summary>
    public string? CoverUrl { get; set; }
  }
}
