using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  /// <summary>
  /// Strategy to work with physical deliveries.
  /// </summary>
  public class ShippingStrategy : IPurchaseOrderStrategy
  {
    private readonly ILogger _logger;

    /// <summary>
    /// Initialize the <see cref="ShippingStrategy"/>.
    /// </summary>
    /// <param name="orderProcessor">Order processor.</param>
    /// <param name="productsRepository">Products repository.</param>
    /// <param name="ordersRepository">Orders repository.</param>
    /// <param name="orderValidator">Order validator.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ShippingStrategy(ILogger<ShippingStrategy> logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Generate shipping slip for the order.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>Task.</returns>
    public async Task ApplyAsync(Order order)
    {
      if (!order.Products.Any(x => x.IsPhysical))
      {
        return;  
      }

      // Shipping slip generation logic.
      // Assuming it is complex and takes time to complete, it might be done by a different service.
      // Here we can, for example, place a message into the queue.
      await Task.Delay(1);

      _logger.LogInformation("Shipping slip generated for the order {OrderId}", order.Id);
    }
  }
}
