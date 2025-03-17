using EShop.Enumerations;
using EShop.Interfaces;
using EShop.Models;

namespace EShop.Services
{
  /// <summary>
  /// Validate order's information.
  /// </summary>
  public class OrderValidator : IOrderValidator
  {
    /// <inheritdoc/>
    public bool IsTotalAmoutValid(Order order)
      => order.TotalAmount > 0 && order.TotalAmount == order.Products.Sum(p => p.Price);

    /// <inheritdoc/>
    public bool IsProductsContentValid(Order order)
      // order cannot contain more than one membership
      => order.Products.Where(p => p.Type == ProductType.Membership).Count() <= 1;
  }
}
