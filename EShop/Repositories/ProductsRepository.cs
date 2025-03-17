using Dapper;
using EShop.Enumerations;
using EShop.Interfaces;
using EShop.Models;
using EShop.Repositories.Abstract;

namespace EShop.Repositories
{
  /// <summary>
  /// Implement CRUD operations with products.  
  /// </summary>
  public class ProductsRepository : RepositoryBase, IProductsRepository
  {
    /// <inheritdoc/>
    public ProductsRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetAllAsync(ProductType? productType = null)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();
      var query = @"
        SELECT Id, Description, Type, Price, IsPhysical, CoverUrl
        FROM Products
        WHERE Display = 1";

      if (productType.HasValue) {
        query += " AND Type = @ProductType";
      }

      return await connection.QueryAsync<Product>(query, new { ProductType = (int?)productType });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetListAsync(IEnumerable<int> productIds) {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryAsync<Product>(@"
        SELECT Id, Description, Type, Price, IsPhysical, CoverUrl
        FROM Products
        WHERE Id IN @Ids
          AND Display = 1",
        new { Ids = productIds });
    }

    /// <inheritdoc/>
    public async Task<int> CreateAsync(Product product)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      product.Id = await connection.ExecuteScalarAsync<int>(@"
        INSERT INTO Products (Description, Type, Price, IsPhysical, CoverUrl)
        VALUES (@Description, @Type, @Price, @IsPhysical, @CoverUrl);
        SELECT CAST(SCOPE_IDENTITY() as int)",
        product);

      return product.Id;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Product product)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      await connection.ExecuteAsync(@"
        UPDATE Products
        SET Description = @Description,
            Price = @Price,
            IsPhysical = @IsPhysical,
            CoverUrl = @CoverUrl
        WHERE Id = @Id",
        product);
    }

    /// <inheritdoc/>
    public async Task DeleteByIdAsync(int id)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      await connection.ExecuteAsync(@"
        UPDATE Products SET Display = 0 WHERE Id = @Id",
        new { Id = id });
    }
  }
}
