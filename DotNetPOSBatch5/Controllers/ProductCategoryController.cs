using DotNetPOS.Domain.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseController
    {
        private readonly ProductCategoryService _service;

        public ProductCategoryController(ProductCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var lst = await _service.GetProductCategories();
            return Execute(lst);
        }

        [HttpGet("{categoryCode}")]
        public async Task<IActionResult> GetProductCategory(string categoryCode)
        {
            var item = await _service.GetProductCategoryByCode(categoryCode);
            return Execute(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductCategory(string categoryName)
        {
            var result = await _service.PostProductCategory(categoryName);
            return Execute(result);
        }

        [HttpPatch("{categoryCode}")]
        public async Task<IActionResult> UpdateProductCategory(string categoryCode, string categoryName)
        {
            var result = await _service.UpdateProductCategory(categoryCode, categoryName);
            return Execute(result);
        }

        [HttpDelete("{categoryCode}")]
        public async Task<IActionResult> DeleteProductCategory(string categoryCode)
        {
            var result = await _service.DeleteProductCategory(categoryCode);
            return Execute(result);
        }
    }
}
