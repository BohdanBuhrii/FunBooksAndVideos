using System.Data;

namespace EShop.Interfaces
{
  public interface IDbConnectionFactory
  {
    Task<IDbConnection> CreateConnectionAsync();
  }
}
