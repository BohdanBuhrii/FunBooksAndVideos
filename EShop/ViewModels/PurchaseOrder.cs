using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
  /// <summary>
  /// Purchase order basic info.
  /// </summary>
  public class PurchaseOrder
  {
    /// <summary>
    /// Total price of items.
    /// </summary>
    public required decimal TotalAmount { get; set; }

    /// <summary>
    /// Customer identifier.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; } // Assuming it is not the id of authenticated user

    /// <summary>
    /// List of product ids.
    /// </summary>
    public required IEnumerable<int> ProductIds { get; set; }
  }
}
