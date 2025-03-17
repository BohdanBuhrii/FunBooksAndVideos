using EShop.Interfaces;

namespace EShop.Repositories.Abstract
{
  /// <summary>
  /// Encapsulate shared repository logic.
  /// </summary>
  public abstract class RepositoryBase
  {
    protected readonly IDbConnectionFactory _connectionFactory;

    /// <summary>
    /// Initialize a repository class.
    /// </summary>
    /// <param name="connectionFactory">Connection factory.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RepositoryBase(IDbConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }
  }
}
