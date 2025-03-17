using Dapper;
using EShop.Interfaces;
using EShop.Models;
using EShop.Repositories.Abstract;

namespace EShop.Repositories
{
  /// <summary>
  /// Manage membership info.
  /// </summary>
  public class MembershipsRepository : RepositoryBase, IMembershipsRepository
  {
    /// <inheritdoc/>
    public MembershipsRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) {}

    /// <inheritdoc/>
    public async Task<Membership?> GetByProductIdAsync(int productId)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryFirstAsync<Membership>(
        "SELECT Id, Description, DurationInDays FROM Memberships WHERE ProductId = @ProductId",
        new { ProductId = productId });
    }

    /// <inheritdoc/>
    public async Task<MembershipInfo?> GetByCustomerIdAsync(int customerId)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryFirstOrDefaultAsync<MembershipInfo>(@"
        SELECT TOP 1 m.Id AS MembershipId, m.Description, cm.ExpirationDate
        FROM Memberships m
        JOIN CustomerMemberships cm
          ON m.Id = cm.MembershipId
        WHERE cm.CustomerId = @CustomerId
        ORDER BY cm.ExpirationDate ASC",
        new { CustomerId = customerId });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryAsync<Membership>(
        "SELECT Id, Description, DurationInDays FROM Memberships");
    }
  }
}
