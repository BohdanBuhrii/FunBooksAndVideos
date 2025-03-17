using EShop.Interfaces;
using EShop.Models;
using System.Transactions;

namespace EShop.Services
{
  public class PurchaseOrderProcessor : IPurchaseOrderProcessor
  {
    private readonly IEnumerable<IPurchaseOrderStrategy> _strategies;

    public PurchaseOrderProcessor(IEnumerable<IPurchaseOrderStrategy> strategies)
    {
      _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
    }

    public async Task<int> CreateOrderAsync(Order order)
    {
      using var scope = new TransactionScope(
        TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
        TransactionScopeAsyncFlowOption.Enabled);

      // Business rules should be executed following the order they are registered in the DI.
      foreach (var strategy in _strategies)
      {
        await strategy.ApplyAsync(order);
      }

      scope.Complete();
      
      return order.Id;
    }
  }
}
