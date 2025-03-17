using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  public class ShippingStrategy : IPurchaseOrderStrategy
  {
    private readonly ILogger _logger;

    public ShippingStrategy(ILogger<ShippingStrategy> logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ApplyAsync(Order order)
    {
      if (!order.Products.Any(x => x.IsPhysical))
      {
        return;  
      }

      // Shipping slip generation logic.
      // Assuming it is complex and might take time to complete, it should be done by a different service.
      // Here we can, for example, place a message into the queue.
      await Task.Delay(1);

      _logger.LogInformation("Shipping slip generated for the order {OrderId}", order.Id);
    }
  }
}
