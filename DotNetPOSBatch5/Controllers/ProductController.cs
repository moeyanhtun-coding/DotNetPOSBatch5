using DotNetPOS.Database.Models;
using DotNetPOS.Domain.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpPost]
        public IActionResult PostProduct( string name, double price, string productCategoryCode)
        {
            var result = productService.ProductCreate(name, price, productCategoryCode);
            if (result < 0)
            {
                return BadRequest(new { message = "Product creation fail" });
            }
            return Ok(new { message = "Product Creation Successful" });
        }

        [HttpPatch("{productCode}")]
        public IActionResult UpdateProduct(string productCode,string name, double price, string productCategoryCode)
        {
            var result = productService.ProductUpdate(productCode, name, price, productCategoryCode);
            if (result < 0)
            {
                return BadRequest(new { message = "Product Update fail" });
            }
            return Ok(new { message = "Product Update Successful" });
        }

        [HttpDelete("{productCode}")]
        public IActionResult DeleteProduct(string productCode)
        {
            var result = productService.ProductDelete(productCode);
            if (result < 0)
            {
                return BadRequest(new { message = "Product creation fail" });
            }
            return Ok(new { message = "Product Creation Successful" });
        }
    }
}
