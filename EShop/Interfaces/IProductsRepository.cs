using EShop.Enumerations;
using EShop.Models;

namespace EShop.Interfaces
{
  /// <summary>
  /// Interface to manage product records.
  /// </summary>
  public interface IProductsRepository
  {
    /// <summary>
    /// Get all products, filtered by type.
    /// </summary>
    /// <param name="productType">Product type.</param>
    /// <returns>List of products.</returns>
    Task<IEnumerable<Product>> GetAllAsync(ProductType? productType = null);

    /// <summary>
    /// Get list of products by product ids.
    /// </summary>
    /// <param name="productIds">List of product identifiers.</param>
    /// <returns>List of products.</returns>
    Task<IEnumerable<Product>> GetListAsync(IEnumerable<int> productIds);

    /// <summary>
    /// Create new product.
    /// </summary>
    /// <param name="product">Product info.</param>
    /// <returns>New product identifier.</returns>
    Task<int> CreateAsync(Product product);

    /// <summary>
    /// Update existing product info, except fot the product type.
    /// </summary>
    /// <param name="product">Product info.</param>
    /// <returns>Task.</returns>
    Task UpdateAsync(Product product);

    /// <summary>
    /// Soft delete the product by id.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns>Task.</returns>
    Task DeleteByIdAsync(int id);
  }
}
