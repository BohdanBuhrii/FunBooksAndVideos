using EShop.Interfaces;
using EShop.Models;
using System.Text;

namespace EShop.Services
{
  public class ShippingSlipBuilder : IShippingSlipBuilder
  {
    private readonly StringBuilder _fileContent;

    public ShippingSlip ShippingSlip { get; private set; }

    public string FileContent => _fileContent.ToString();

    public ShippingSlipBuilder()
    {
      _fileContent = new StringBuilder();
      ShippingSlip = new ShippingSlip();
    }

    public void AddOrderInformation() { }
    public void AddCustomerAddress() { }
    public void AddPickupPointAddress() { }
    public void AddCustomerDetails() { }
    public void UseDeliveryPinCode() { }
    public void AddTrackingCode() { }
    public void UsePremiumDelivery() { }
  }
}
