using EShop.Interfaces;
using EShop.Models;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly IPurchaseOrderProcessor _orderProcessor;
    private readonly IProductsRepository _productsRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderValidator _orderValidator;

    public OrdersController(
      IPurchaseOrderProcessor orderProcessor,
      IProductsRepository productsRepository,
      IOrdersRepository ordersRepository,
      IOrderValidator orderValidator)
    {
      _orderProcessor = orderProcessor ?? throw new ArgumentNullException(nameof(orderProcessor));
      _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
      _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
      _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateOrder([FromBody] PurchaseOrder purchaseOrder)
    {
      var order = new Order
      {
        TotalAmount = purchaseOrder.TotalAmount,
        CustomerId = purchaseOrder.CustomerId,
        Products = await _productsRepository.GetListAsync(purchaseOrder.ProductIds)
      };

      ValidateOrder(order);

      if (!ModelState.IsValid) {
        return BadRequest(ModelState);
      }

      var id = await _orderProcessor.CreateOrderAsync(order);

      return CreatedAtAction(nameof(GetOrder), new { id }, id);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<IActionResult> GetOrder([FromRoute][Range(1, int.MaxValue)] int id)
    {
      var order = await _ordersRepository.GetOrderWithProductsAsync(id);

      return order is null ? NotFound() : Ok(order);
    }

    private void ValidateOrder(Order order)
    {
      if (!_orderValidator.IsTotalAmoutValid(order))
      {
        ModelState.AddModelError(nameof(order.TotalAmount), "The amount should match the price of products");
      }

      if (!_orderValidator.IsProductsContentValid(order))
      {
        ModelState.AddModelError(nameof(order.Products), "Only one Membership per order is allowed");
      }
    }
  }
}
