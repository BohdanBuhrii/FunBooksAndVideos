using EShop.Services;
using EShop.Models;
using EShop.Enumerations;

namespace EShop.Tests.Services
{
  public class OrderValidatorTests
  {
    private readonly OrderValidator _orderValidator;

    public OrderValidatorTests()
    {
      _orderValidator = new OrderValidator();
    }

    public static IEnumerable<object[]> TotalAmountTestData =>
    [
      [new Order { TotalAmount = 100, Products = [new Product { Price = 50 }, new Product { Price = 50 }] }, true],
      [new Order { TotalAmount = 100, Products = [new Product { Price = 40 }, new Product { Price = 50 }] }, false],
      [new Order { TotalAmount = -10, Products = [new Product { Price = -10 }] }, false ],
      [new Order { TotalAmount = 0, Products = [] }, false]
    ];

    [Theory]
    [MemberData(nameof(TotalAmountTestData))]
    public void IsTotalAmountValid_ShouldValidateCorrectly(Order order, bool expected)
    {
      // Act
      var result = _orderValidator.IsTotalAmoutValid(order);

      // Assert
      Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> ProductsContentTestData =>
    [
      [new Order { Products = [new Product { Type = ProductType.Membership }, new Product { Type = ProductType.Book }] }, true],
      [new Order { Products = [new Product { Type = ProductType.Membership }, new Product { Type = ProductType.Membership }] }, false],
      [new Order { Products = [new Product { Type = ProductType.Movie }] }, true]
    ];

    [Theory]
    [MemberData(nameof(ProductsContentTestData))]
    public void IsProductsContentValid_ShouldValidateCorrectly(Order order, bool expected)
    {
      // Act
      var result = _orderValidator.IsProductsContentValid(order);

      // Assert
      Assert.Equal(expected, result);
    }
  }
}
