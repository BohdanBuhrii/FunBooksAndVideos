namespace EShop.Models
{
  public class CustomerShippingPreferences
  {
    public string? CustomerAddress { get; set; }

    public string? PickupPointAddress { get; set; }

    /// <summary>
    /// If set to true, the parcel should be delivered to customer's address.
    /// </summary>
    public bool HomeDelivery { get; set; }

    /// <summary>
    /// Indicates whether customer details (name, email, etc.) are included.
    /// </summary>
    public bool AnonymousParcel { get; set; }

    /// <summary>
    /// True, if parcel tracking is enabled.
    /// </summary>
    //public bool IncludeTracking { get; set; }
  }
}
