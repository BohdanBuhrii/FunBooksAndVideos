using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Interface to manage orders.
  /// </summary>
  public interface IOrdersRepository
  {
    /// <summary>
    /// Create order record and set a new identifier.
    /// </summary>
    /// <param name="order">Order info.</param>
    /// <returns>Task.</returns>
    Task CreateAsync(Order order);

    /// <summary>
    /// Get order with products' info.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <returns>Extended order information.</returns>
    Task<Order?> GetOrderWithProductsAsync(int id);

    /// <summary>
    /// Get all orders without nested entities.
    /// </summary>
    /// <returns>List of orders.</returns>
    Task<IEnumerable<Order>> GetAllAsync();
  }
}
