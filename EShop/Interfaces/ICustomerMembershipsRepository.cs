using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Manage CustomerMemberships link table.
  /// </summary>
  public interface ICustomerMembershipsRepository
  {
    /// <summary>
    /// Assign membership to the customer.
    /// </summary>
    /// <param name="membership">Customer identifier.</param>
    /// <returns>Task.</returns>
    Task CreateAsync(CustomerMembership membership);
  }
}
