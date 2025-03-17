using Dapper;
using EShop.Interfaces;
using EShop.Models;
using EShop.Repositories.Abstract;

namespace EShop.Repositories
{
  public class MembershipsRepository : RepositoryBase, IMembershipsRepository
  {
    public MembershipsRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) {}

    public async Task<Membership?> GetByProductIdAsync(int productId)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryFirstAsync<Membership>(
        "SELECT Id, Description, DurationInDays FROM Memberships WHERE ProductId = @ProductId",
        new { ProductId = productId });
    }

    public async Task<MembershipInfo?> GetByCustomerIdAsync(int customerId)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryFirstOrDefaultAsync<MembershipInfo>(@"
        SELECT m.Id AS MembershipId, m.Description, cm.ExpirationDate
        FROM Memberships m
        JOIN CustomerMemberships cm
          ON m.Id = cm.MembershipId
        WHERE cm.CustomerId = @CustomerId",
        new { CustomerId = customerId });
    }

    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      return await connection.QueryAsync<Membership>(
        "SELECT Id, Description, DurationInDays FROM Memberships");
    }
  }
}
