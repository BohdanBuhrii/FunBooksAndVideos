using EShop.Interfaces;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MembershipsController : ControllerBase
  {
    private readonly IMembershipsRepository _membershipsRepository;

    public MembershipsController(IMembershipsRepository membershipsRepository)
    {
      _membershipsRepository = membershipsRepository ?? throw new ArgumentNullException(nameof(membershipsRepository));
    }

    [HttpGet("/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Membership>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllMemberships()
    {
      return Ok(await _membershipsRepository.GetAllAsync());
    }

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
