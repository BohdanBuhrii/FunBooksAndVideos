using Dapper;
using EShop.Interfaces;
using EShop.Models;
using EShop.Repositories.Abstract;

namespace EShop.Repositories
{
  public class OrdersRepository : RepositoryBase, IOrdersRepository
  {
    public OrdersRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task CreateAsync(Order order)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();
      using var transaction = connection.BeginTransaction();

      try
      {
        // insert order record
        order.Id = await connection.ExecuteScalarAsync<int>(@"
          INSERT INTO Orders (TotalAmount, CustomerId) VALUES (@TotalAmount, @CustomerId);
          SELECT CAST(SCOPE_IDENTITY() as int)",
          order, transaction);

        // create order-product links
        await connection.ExecuteAsync(
          "INSERT INTO OrderProducts (OrderId, ProductId) VALUES (@OrderId, @ProductId)",
          order.Products.Select(product => new { OrderId = order.Id, ProductId = product.Id }),
          transaction);

        transaction.Commit();
      }
      catch
      {
        transaction.Rollback();
        throw;
      }

    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryAsync<Order>(
        "SELECT Id, TotalAmount, CustomerId FROM Orders");
    }

    public async Task<Order?> GetOrderWithProductsAsync(int id)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      var sql = @"
        SELECT Id, TotalAmount, CustomerId
        FROM Orders
        WHERE Id = @OrderId;
        
        SELECT Id, Description, Type, Price, IsPhysical, CoverUrl
        FROM Products p
        JOIN OrderProducts op
          ON p.Id = op.ProductId
        WHERE op.OrderId = @OrderId";

      using var multi = await connection.QueryMultipleAsync(sql, new { OrderId = id });

      var order = await multi.ReadFirstOrDefaultAsync<Order>();
      
      if (order != null)
      {
        order.Products = await multi.ReadAsync<Product>();
      }
      
      return order;
    }
  }
}
