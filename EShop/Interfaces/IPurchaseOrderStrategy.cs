using EShop.Models;

namespace EShop.Interfaces
{
  public interface IPurchaseOrderStrategy
  {
    Task ApplyAsync(Order order);
  }
}
