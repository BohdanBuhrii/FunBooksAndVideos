using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Manage shipping slips db records.
  /// </summary>
  public interface IShippingSlipsRepository
  {
    /// <summary>
    /// Save shipping slip info.
    /// </summary>
    /// <param name="shippingSlip">Shipping slip.</param>
    /// <returns>Task.</returns>
    Task CreateAsync(ShippingSlip shippingSlip);
  }
}
