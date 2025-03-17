using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  /// <summary>
  /// Strategy to create order record.
  /// </summary>
  public class CreateOrderStrategy : IPurchaseOrderStrategy
  {
    private readonly IOrdersRepository _ordersRepository;

    /// <summary>
    /// Initialize the <see cref="CreateOrderStrategy"/>.
    /// </summary>
    /// <param name="ordersRepository">Orders repository.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public CreateOrderStrategy(IOrdersRepository ordersRepository)
    {
      _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
    }

    /// <summary>
    /// Create order record in the db and set new identifier.
    /// </summary>
    /// <param name="order">Order info.</param>
    /// <returns>Task.</returns>
    public Task ApplyAsync(Order order) => _ordersRepository.CreateAsync(order);
  }
}
