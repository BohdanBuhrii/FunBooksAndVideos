using EShop.Interfaces;
using EShop.Models;
using System.Transactions;

namespace EShop.Services
{
  /// <summary>
  ///  Purchase order processor.
  /// </summary>
  public class PurchaseOrderProcessor : IPurchaseOrderProcessor
  {
    private readonly IEnumerable<IPurchaseOrderStrategy> _strategies;

    /// <summary>
    /// Initialize the <see cref="PurchaseOrderProcessor"/>.
    /// </summary>
    /// <param name="strategies">List of available purchase order strategies.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public PurchaseOrderProcessor(IEnumerable<IPurchaseOrderStrategy> strategies)
    {
      _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
    }

    /// <inheritdoc/>
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
