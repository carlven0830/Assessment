using JrAssessment.Core.Services;
using JrAssessment.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : Controller
    {
        private readonly IGuestService _guestServ;
        public GuestController(IGuestService guestServ)
        {
            _guestServ = guestServ;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterGuest([FromBody] RegisterGuestRequest request)
        {
            var resp = await _guestServ.RegisterGuestAsync(request);

            return Ok(resp);
        }
    }
}
