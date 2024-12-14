using DotNetPOS.Domain.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryService _service;

        public ProductCategoryController(ProductCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProductCategories()
        {
            var lst = _service.GetProductCategories();
            return Ok(new { data = lst });
        }

        [HttpGet("{categoryCode}")]
        public IActionResult GetProductCategory(string categoryCode)
        {
            var item = _service.GetProductCategoryByCode(categoryCode);
            if (item is null) return NotFound(new { message = "Category Not Found" });
            return Ok(new { data = item });
        }

        [HttpPost]
        public IActionResult CreateProductCategory(string categoryName)
        {
            var result = _service.PostProductCategory(categoryName);
            if (result is 0)
                return BadRequest(new { message = "Category Creation is Fail" });
            return Ok(new { message = "Category Creation is Successful" });
        }

        [HttpPatch("{categoryCode}")]
        public IActionResult UpdateProductCategory(string categoryCode, string categoryName)
        {
            var result = _service.UpdateProductCategory(categoryCode, categoryName);
            if (result is 0) return BadRequest(new { message = "Update Fail" });
            return Ok(new { message = "Updating Successful" });
        }

        [HttpDelete("{categoryCode}")]
        public IActionResult DeleteProductCategory(string categoryCode)
        {
            var result = _service.DeleteProductCategory(categoryCode);
            if (result is 0) return BadRequest(new { message = "Deleting fail" });
            return Ok(new { message = "Deleting Successful" });

        }
    }
}
