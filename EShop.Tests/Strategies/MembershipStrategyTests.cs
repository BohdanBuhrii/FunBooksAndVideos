using Moq;
using Microsoft.Extensions.Logging;
using EShop.Strategies;
using EShop.Interfaces;
using EShop.Models;
using EShop.Enumerations;

namespace EShop.Tests.Strategies
{
  public class MembershipStrategyTests
  {

    private readonly MockRepository _repo;
    private readonly Mock<IMembershipsRepository> _mockMembershipsRepository;
    private readonly Mock<ICustomerMembershipsRepository> _mockCustomerMembershipsRepository;
    private readonly Mock<ILogger<MembershipStrategy>> _mockLogger;
    private readonly MembershipStrategy _membershipStrategy;

    public MembershipStrategyTests()
    {
      _repo = new MockRepository(MockBehavior.Strict);

      _mockMembershipsRepository = _repo.Create<IMembershipsRepository>();
      _mockCustomerMembershipsRepository = _repo.Create<ICustomerMembershipsRepository>();
      _mockLogger = _repo.Create<ILogger<MembershipStrategy>>();

      _membershipStrategy = new MembershipStrategy(
          _mockMembershipsRepository.Object,
          _mockCustomerMembershipsRepository.Object,
          _mockLogger.Object
      );
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorData))]
    public void Constructor_ShouldThrowArgumentNullException_WhenDependenciesAreNull(
        IMembershipsRepository membershipsRepository,
        ICustomerMembershipsRepository customerMembershipsRepository,
        ILogger<MembershipStrategy> logger)
    {
      // Act & Assert
      var exception = Assert.Throws<ArgumentNullException>(() =>
          new MembershipStrategy(membershipsRepository, customerMembershipsRepository, logger));
    }

    public static IEnumerable<object[]> InvalidConstructorData()
    {
      var mRepo = Mock.Of<IMembershipsRepository>();
      var cmRepo = Mock.Of<ICustomerMembershipsRepository>();
      var logger = Mock.Of<ILogger<MembershipStrategy>>();

      return [[null!, cmRepo, logger], [mRepo, null!, logger], [mRepo, cmRepo, null!]];
    }


    [Fact]
    public async Task ApplyAsync_ShouldDoNothing_WhenNoMembershipInOrder()
    {
      // Arrange
      var order = new Order
      {
        CustomerId = 11,
        Products = [new Product { Type = ProductType.Other }]
      };

      // Act
      await _membershipStrategy.ApplyAsync(order);

      // Assert
      _repo.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ApplyAsync_ShouldThrowException_WhenMembershipNotFound()
    {
      // Arrange
      var productId = 123;
      var order = new Order
      {
        CustomerId = 11,
        Products = [new Product { Type = ProductType.Membership, Id = productId }]
      };

      _mockMembershipsRepository
        .Setup(r => r.GetByProductIdAsync(productId))
        .ReturnsAsync(null as Membership); // Membership not found

      // Act & Assert
      var exception = await Assert.ThrowsAsync<Exception>(() => _membershipStrategy.ApplyAsync(order));
      Assert.Equal($"Membership not found. ProductId: {productId}", exception.Message);
      _repo.VerifyAll();
      _repo.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ApplyAsync_ShouldCreateCustomerMembership_WhenMembershipFound()
    {
      // Arrange
      var order = new Order
      {
        CustomerId = 11,
        Products = [new Product { Type = ProductType.Membership, Id = 15 }]
      };

      var membership = new Membership
      {
        Id = 7,
        DurationInDays = 30,
        Description = "Test Membership"
      };

      _mockMembershipsRepository
        .Setup(r => r.GetByProductIdAsync(order.Products.First().Id))
        .ReturnsAsync(membership);

      _mockCustomerMembershipsRepository
        .Setup(r => r.CreateAsync(It.Is<CustomerMembership>(cm => cm.CustomerId == order.CustomerId
                                                         && cm.MembershipId == membership.Id)))
        .Returns(Task.CompletedTask);

      _mockLogger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(),
        It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

      // Act
      await _membershipStrategy.ApplyAsync(order);

      // Assert
      _repo.VerifyAll();
      _repo.VerifyNoOtherCalls();
    }
  }
}
