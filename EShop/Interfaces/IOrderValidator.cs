using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Validate order info.
  /// </summary>
  public interface IOrderValidator
  {
    /// <summary>
    /// Validate order's total amount.
    /// </summary>
    /// <param name="order">Order info.</param>
    /// <returns>True if the order is valid, otherwise false.</returns>
    bool IsTotalAmoutValid(Order order);

    /// <summary>
    /// Validate order's products list.
    /// </summary>
    /// <param name="order">Order info.</param>
    /// <returns>True if the order is valid, otherwise false.</returns>
    bool IsProductsContentValid(Order order);
  }
}
