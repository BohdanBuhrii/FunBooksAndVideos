using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Interface to manage membeships.
  /// </summary>
  public interface IMembershipsRepository
  {
    /// <summary>
    /// Get all available memberships.
    /// </summary>
    /// <returns>List of memberships.</returns>
    Task<IEnumerable<Membership>> GetAllAsync();

    /// <summary>
    /// Get membership by linked product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <returns>Membership information.</returns>
    Task<Membership?> GetByProductIdAsync(int productId);

    /// <summary>
    /// Get info about customer's active membership.
    /// </summary>
    /// <param name="customerId">Customer identifier.</param>
    /// <returns>Customer-specific membeship info.</returns>
    Task<MembershipInfo?> GetByCustomerIdAsync(int customerId);
  }
}
