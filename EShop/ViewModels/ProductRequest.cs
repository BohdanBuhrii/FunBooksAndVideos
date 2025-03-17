using EShop.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
  public class ProductRequest
  {
    [MaxLength(255)]
    public string? Description { get; set; }

    public ProductType Type { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    public bool IsPhysical { get; set; }

    [MaxLength(2048)]
    public string? CoverUrl { get; set; }
  }
}
