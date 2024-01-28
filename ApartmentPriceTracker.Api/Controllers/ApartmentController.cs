using ApartmentPriceTracker.Api.Models;
using ApartmentPriceTracker.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentPriceTracker.Api.Controllers
{
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _service;

        public ApartmentController(IApartmentService service)
        {
            _service = service;
        }
        [HttpGet("GetApartmentPrices")]
        public IActionResult GetApartmentPrices()
        {
            var apartments = _service.GetApartments();
            if (apartments.Any())
                return Ok(apartments);
            return NotFound();
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.SaveSubscriptionAsync(subscription.ApartmentUrl, subscription.Email);
            return Ok();
        }
    }
}
