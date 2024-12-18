using DotNetPOS.Database.Models;
using DotNetPOS.Domain.Features;
using DotNetPOS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductServiceController : BaseController
    {
        private readonly ProductService _productService;

        public ProductServiceController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var productsList = await _productService.GetProductsAsync();
            return Execute(productsList);
        }

        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetProductWithCode(string productCode)
        {
            var product = await _productService.GetProductWithCode(productCode);
            return Execute(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequestModel product)
        {
            var newProduct = await _productService.CreateProduct(product);
            return Execute(newProduct); 
        }

        [HttpPatch("{productCode}")]
        public async Task<IActionResult> ChangeProductInfo(string productCode, ChangeProductInfoRequestModel productInfo)
        {
            var changedProduct = await _productService.ChangeProductInfo(productCode, productInfo);
            return Execute(changedProduct);
        }

        [HttpDelete("{productCode}")]
        public async Task<IActionResult> DeleteProduct(string productCode)
        {
            var result = await _productService.DeleteProduct(productCode); 
            return Execute(result);
        }
    }
}
