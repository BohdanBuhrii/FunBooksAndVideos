using Dapper;
using EShop.Interfaces;
using EShop.Models;
using EShop.Repositories.Abstract;

namespace EShop.Repositories
{
  /// <summary>
  /// Manage customer-membership links.
  /// </summary>
  public class CustomerMembershipsRepository : RepositoryBase, ICustomerMembershipsRepository
  {
    /// <inheritdoc/>
    public CustomerMembershipsRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    /// <inheritdoc/>
    public async Task CreateAsync(CustomerMembership membership)
    {
      using var connection = await _connectionFactory.CreateConnectionAsync();

      await connection.ExecuteAsync(@"
        INSERT INTO CustomerMemberships (CustomerId, MembershipId, ExpirationDate)
        VALUES (@CustomerId, @MembershipId, @ExpirationDate)",
        membership);
    }
  }
}
