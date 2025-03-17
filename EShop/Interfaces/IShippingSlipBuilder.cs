using EShop.Models;
namespace EShop.Interfaces
{
  public interface IShippingSlipBuilder
  {
    ShippingSlip ShippingSlip { get; }
    string FileContent { get; }

    void AddOrderInformation(Order order);
    void AddCustomerAddress(string address);
    void AddPickupPointAddress(string address);
    void AddCustomerDetails(int customerId);
    void UseDeliveryPinCode();
    void AddTrackingCode();
    void UsePremiumDelivery();
  }
}
