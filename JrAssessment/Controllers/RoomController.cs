using JrAssessment.Core.Services;
using JrAssessment.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomServ;

        public RoomController(IRoomService roomServ)
        {
            _roomServ = roomServ;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRoom([FromBody] AddRoomRequest request)
        {
            var resp = await _roomServ.AddRoomAsync(request);

            return Ok(resp);
        }
    }
}
