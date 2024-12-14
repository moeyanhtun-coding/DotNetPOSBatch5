using DotNetPOS.Domain.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService = new ProductService();

        [HttpGet]
        public IActionResult GetProducts()
        {
            var lst = productService.GetProducts();
            if (lst is null)
            {
                return Ok(new { message = "Product List is Empty" });
            }
            return Ok(lst);
        }

        [HttpGet("{productCode}")]
        public IActionResult GetProduct(string productCode)
        {
            var item = productService.GetProduct(productCode);
            if (item is null) return NotFound(new {message = "Product Not Found"});
            return Ok(item);
        }
    }
}
