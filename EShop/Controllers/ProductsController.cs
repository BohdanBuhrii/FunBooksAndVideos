using EShop.Enumerations;
using EShop.Interfaces;
using EShop.Models;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
  /// <summary>
  /// Products controller.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductsRepository _productsRepository;

    /// <summary>
    /// Initialize the <see cref="ProductsController"/>.
    /// </summary>
    /// <param name="productsRepository">Products repository.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProductsController(IProductsRepository productsRepository)
    {
      _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    /// <summary>
    /// Get available products. Can be filtered by product type.
    /// </summary>
    /// <param name="productType">Product type.</param>
    /// <returns>List of products.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProducts([FromQuery] ProductType? productType) {
      return Ok(await _productsRepository.GetAllAsync(productType));
    }

    /// <summary>
    /// Create new product.
    /// </summary>
    /// <param name="productRequest">Product information.</param>
    /// <returns>No content.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
    {
      // Note: This endpoint can create only "generic" products.
      // For example, membership details (Memberships table) can't be added here.
      var id = await _productsRepository.CreateAsync(Map(productRequest));

      return Created(string.Empty, new { id });
    }

    /// <summary>
    /// Update existing product info except for the product type.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="productRequest">Product info.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateProduct([FromRoute][Range(1, int.MaxValue)] int id, [FromBody] ProductRequest productRequest)
    {
      var product = Map(productRequest);
      product.Id = id;

      await _productsRepository.UpdateAsync(product);

      return NoContent();
    }

    /// <summary>
    /// Soft delete for the product.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteProduct([FromRoute][Range(1, int.MaxValue)] int id)
    {
      await _productsRepository.DeleteByIdAsync(id);

      return NoContent();
    }

    /// <summary>
    /// Map view model to db representation.
    /// </summary>
    /// <param name="request"><see cref="ProductRequest"/></param>
    /// <returns><see cref="Product"/></returns>
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
