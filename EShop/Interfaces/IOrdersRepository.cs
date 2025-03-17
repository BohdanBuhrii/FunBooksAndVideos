using EShop.Models;

namespace EShop.Interfaces
{
  public interface IOrdersRepository
  {
    Task CreateAsync(Order order);

    Task<Order?> GetOrderWithProductsAsync(int id);

    Task<IEnumerable<Order>> GetAllAsync();
  }
}
