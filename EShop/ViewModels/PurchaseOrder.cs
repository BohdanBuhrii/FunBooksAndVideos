using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels
{
  public class PurchaseOrder
  {
    public required decimal TotalAmount { get; set; }

    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; } // Assuming it is not the id of authenticated user

    public required IEnumerable<int> ProductIds { get; set; }
  }
}
