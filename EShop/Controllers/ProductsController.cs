using EShop.Enumerations;
using EShop.Interfaces;
using EShop.Models;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductsRepository _productsRepository;

    public ProductsController(IProductsRepository productsRepository)
    {
      _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProducts([FromQuery] ProductType? productType) {
      return Ok(await _productsRepository.GetAllAsync(productType));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
    {
      // Note: This endpoint can create only "generic" products.
      // For example, membership details (Memberships table) can't be added here.
      var id = await _productsRepository.CreateAsync(Map(productRequest));

      return Created(string.Empty, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute][Range(1, int.MaxValue)] int id, [FromBody] ProductRequest productRequest)
    {
      var product = Map(productRequest);
      product.Id = id;

      await _productsRepository.UpdateAsync(product);

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteProduct([FromRoute][Range(1, int.MaxValue)] int id)
    {
      await _productsRepository.DeleteByIdAsync(id);

      return NoContent();
    }

    private static Product Map(ProductRequest request) => new()
    {
      Description = request.Description,
      Type = request.Type,
      Price = request.Price,
      IsPhysical =request.IsPhysical,
      CoverUrl = request.CoverUrl
    };
  }
}
