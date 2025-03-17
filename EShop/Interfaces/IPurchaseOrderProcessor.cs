using EShop.Models;
namespace EShop.Interfaces
{
  public interface IPurchaseOrderProcessor
  {
    Task<int> CreateOrderAsync(Order order);
  }
}
