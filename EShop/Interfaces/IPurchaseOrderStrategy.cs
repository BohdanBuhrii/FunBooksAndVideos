using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Defines interface for order's business rules.
  /// </summary>
  public interface IPurchaseOrderStrategy
  {
    /// <summary>
    /// Apply the business rule to the order.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>Task.</returns>
    Task ApplyAsync(Order order);
  }
}
