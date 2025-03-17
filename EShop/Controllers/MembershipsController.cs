using EShop.Interfaces;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
  /// <summary>
  /// Memberships controller.
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class MembershipsController : ControllerBase
  {
    private readonly IMembershipsRepository _membershipsRepository;

    /// <summary>
    /// Initialize the <see cref="MembershipsController"/>.
    /// </summary>
    /// <param name="membershipsRepository">Membership repository.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MembershipsController(IMembershipsRepository membershipsRepository)
    {
      _membershipsRepository = membershipsRepository ?? throw new ArgumentNullException(nameof(membershipsRepository));
    }

    /// <summary>
    /// Get all available membeships.
    /// </summary>
    /// <returns>List of memberships.</returns>
    [HttpGet("/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Membership>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllMemberships()
    {
      return Ok(await _membershipsRepository.GetAllAsync());
    }

    /// <summary>
    /// Get customer's membership info.
    /// </summary>
    /// <param name="customerId">Customer identifier.</param>
    /// <returns>Membership info.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembershipInfo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<IActionResult> GetMembership([FromQuery][Range(1, int.MaxValue)] int customerId)
    {
      var membership = await _membershipsRepository.GetByCustomerIdAsync(customerId);
      
      return membership is null ? NotFound() : Ok(membership);
    }
  }
}
