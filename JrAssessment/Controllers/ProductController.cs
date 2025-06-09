using JrAssessment.Core.Services;
using JrAssessment.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace JrAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productServ;

        public ProductController(IProductService productServ)
        {
            _productServ = productServ;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
        {
            var resp = await _productServ.AddProductAsync(request);

            return Ok(resp);
        }
    }
}
