namespace EShop.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class ShippingSlip
  {
    /// <summary>
    /// Order identifier.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Delivery address
    /// </summary>
    public string DeliveryAddress { get; set; } = string.Empty;

    /// <summary>
    /// Additional receiver information or a pin code.
    /// </summary>
    public string ReceiverDetails { get; set; } = string.Empty;

    /// <summary>
    /// Code that can be used to track delivery status.
    /// </summary>
    public string? TrackingCode { get; set; }

    /// <summary>
    /// Indicates whether premium delivery is used.
    /// </summary>
    public bool PremiumDelivery { get; set; }
  }
}
