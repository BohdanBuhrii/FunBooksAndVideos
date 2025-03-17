using EShop.Enumerations;
using EShop.Models;

namespace EShop.Interfaces
{
  public interface IProductsRepository
  {
    Task<IEnumerable<Product>> GetAllAsync(ProductType? productType = null);
    
    Task<IEnumerable<Product>> GetListAsync(IEnumerable<int> productIds);

    Task<int> CreateAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteByIdAsync(int id);
  }
}
