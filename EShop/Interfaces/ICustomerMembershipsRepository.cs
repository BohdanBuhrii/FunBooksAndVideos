using EShop.Models;

namespace EShop.Interfaces
{
  public interface ICustomerMembershipsRepository
  {
    Task CreateAsync(CustomerMembership membership);
  }
}
