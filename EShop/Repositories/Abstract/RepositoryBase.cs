using EShop.Interfaces;

namespace EShop.Repositories.Abstract
{
  public abstract class RepositoryBase
  {
    protected readonly IDbConnectionFactory _connectionFactory;

    public RepositoryBase(IDbConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }
  }
}
