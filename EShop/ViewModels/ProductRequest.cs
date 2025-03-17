using EShop.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
  /// <summary>
  /// Product request.
  /// </summary>
  public class ProductRequest
  {
    /// <summary>
    /// Short description.
    /// </summary>
    [MaxLength(255)]
    public string? Description { get; set; }

    /// <summary>
    /// Product type.
    /// </summary>
    public ProductType Type { get; set; }

    /// <summary>
    /// Price per unit.
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    /// <summary>
    /// Indicates whether product is eligible for physical shipping.
    /// </summary>
    public bool IsPhysical { get; set; }

    /// <summary>
    /// Url to the cover image for the product.
    /// </summary>
    [MaxLength(2048)]
    public string? CoverUrl { get; set; }
  }
}
