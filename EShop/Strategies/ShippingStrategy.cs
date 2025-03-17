using EShop.Interfaces;
using EShop.Models;

namespace EShop.Strategies
{
  /// <summary>
  /// Strategy to work with physical deliveries.
  /// </summary>
  public class ShippingStrategy : IPurchaseOrderStrategy
  {
    private readonly IShippingSlipsRepository _slipsRepository;
    private readonly IShippingSlipBuilder _slipBuilder;
    private readonly IFilesStorage _filesStorage;
    private readonly IMembershipsRepository _membershipsRepository;
    private readonly ILogger _logger;

    /// <summary>
    /// Initialize the <see cref="ShippingStrategy"/>.
    /// </summary>
    /// <param name="slipsRepository">Shipping slips repository.</param>
    /// <param name="slipBuilder">Shipping slip builder.</param>
    /// <param name="filesStorage">Files storage.</param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ShippingStrategy(
      IShippingSlipsRepository slipsRepository,
      IShippingSlipBuilder slipBuilder,
      IFilesStorage filesStorage,
      IMembershipsRepository membershipsRepository,
      ILogger<ShippingStrategy> logger)
    {
      _slipsRepository = slipsRepository ?? throw new ArgumentNullException(nameof(slipsRepository));
      _slipBuilder = slipBuilder ?? throw new ArgumentNullException(nameof(slipBuilder));
      _filesStorage = filesStorage ?? throw new ArgumentNullException(nameof(filesStorage));
      _membershipsRepository = membershipsRepository ?? throw new ArgumentNullException(nameof(membershipsRepository));

      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Generate shipping slip for the order.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>Task.</returns>
    public async Task ApplyAsync(Order order)
    {
      if (!order.Products.Any(x => x.IsPhysical))
      {
        return;
      }

      await GenerateShippingSlip(order);

      // ensure file is saved before db updates
      await _filesStorage.SaveFile("shipping_slips", $"{order.Id}.txt", _slipBuilder.FileContent);
      await _slipsRepository.CreateAsync(_slipBuilder.ShippingSlip);

      _logger.LogInformation("Shipping slip generated for the order {OrderId}", order.Id);
    }

    /// <summary>
    /// Generate shipping slip for the order.
    /// </summary>
    /// <param name="order">Order information.</param>
    /// <returns>Task.</returns>
    private async Task GenerateShippingSlip(Order order) {
      var preferences = new CustomerShippingPreferences();// TODO get shipping preferences from db

      _slipBuilder.AddOrderInformation(order);

      if (preferences.HomeDelivery)
      {
        _slipBuilder.AddCustomerAddress(preferences.CustomerAddress!);
      }
      else
      {
        _slipBuilder.AddPickupPointAddress(preferences.PickupPointAddress!);
      }

      if (preferences.AnonymousParcel)
      {
        _slipBuilder.UseDeliveryPinCode();
      }
      else
      {
        _slipBuilder.AddCustomerDetails(order.CustomerId);
      }

      _slipBuilder.AddTrackingCode();

      var membership = await _membershipsRepository.GetByCustomerIdAsync(order.CustomerId);

      if (membership?.MembershipId == 3) // premium, TODO get rid of hardcoded value
      {
        _slipBuilder.UsePremiumDelivery();
      }
    }
  }
}
