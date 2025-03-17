using EShop.Enumerations;
using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  /// <summary>
  /// If the order contains membership, it is processed by this strategy.
  /// </summary>
  public class MembershipStrategy : IPurchaseOrderStrategy
  {
    private readonly IMembershipsRepository _membershipsRepository;
    private readonly ICustomerMembershipsRepository _customerMembershipsRepository;
    private readonly ILogger _logger;

    /// <summary>
    /// Initialize the <see cref="MembershipStrategy"/>.
    /// </summary>
    /// <param name="membershipsRepository">Memberships repository.</param>
    /// <param name="customerMembershipsRepository">Customer memberships repository.</param>
    /// <param name="logger">Logger.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MembershipStrategy(
      IMembershipsRepository membershipsRepository,
      ICustomerMembershipsRepository customerMembershipsRepository,
      ILogger<MembershipStrategy> logger)
    {
      _membershipsRepository = membershipsRepository ?? throw new ArgumentNullException(nameof(membershipsRepository));
      _customerMembershipsRepository = customerMembershipsRepository ?? throw new ArgumentNullException(nameof(customerMembershipsRepository));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Activate customer's membership.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>Task.</returns>
    /// <exception cref="Exception"></exception>
    public async Task ApplyAsync(Order order)
    {
      // I assume that there can be only one active membership per user.
      var purchasedMembership = order.Products.FirstOrDefault(x => x.Type == ProductType.Membership);

      if (purchasedMembership is null)
      {
        return;
      }

      var membership = await _membershipsRepository.GetByProductIdAsync(purchasedMembership.Id)
        ?? throw new Exception($"Membership not found. ProductId: {purchasedMembership.Id}");

      await _customerMembershipsRepository.CreateAsync(new CustomerMembership
      {
        CustomerId = order.CustomerId,
        MembershipId = membership.Id,
        ExpirationDate = DateTimeOffset.UtcNow.AddDays(membership.DurationInDays)
      });

      _logger.LogInformation("Membership \"{Membership}\" activated for the customer {CustomerId}",
        membership.Description, order.CustomerId);
    }
  }
}
