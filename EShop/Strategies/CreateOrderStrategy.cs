using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  public class CreateOrderStrategy : IPurchaseOrderStrategy
  {
    private readonly IOrdersRepository _ordersRepository;

    public CreateOrderStrategy(IOrdersRepository ordersRepository)
    {
      _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
    }

    public Task ApplyAsync(Order order) => _ordersRepository.CreateAsync(order);
  }
}
