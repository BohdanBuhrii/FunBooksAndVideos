using EShop.Models;

namespace EShop.Interfaces
{
  public interface IOrderValidator
  {
    bool IsTotalAmoutValid(Order order);

    bool IsProductsContentValid(Order order);
  }
}
