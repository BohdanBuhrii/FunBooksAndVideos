using EShop.Models;

namespace EShop.Interfaces
{
  public interface IMembershipsRepository
  {
    Task<IEnumerable<Membership>> GetAllAsync();
    Task<Membership?> GetByProductIdAsync(int productId);
    Task<MembershipInfo?> GetByCustomerIdAsync(int customerId);
  }
}
