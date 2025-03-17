using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Purchase order processor.
  /// </summary>
  public interface IPurchaseOrderProcessor
  {
    /// <summary>
    /// Create and process new order.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>New order identifier.</returns>
    Task<int> CreateOrderAsync(Order order);
  }
}
