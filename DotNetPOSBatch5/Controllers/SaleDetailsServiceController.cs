using DotNetPOS.Domain.Features;
using DotNetPOS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailsServiceController : BaseController
    {
        private readonly SaleDetailsService _saleDetailsService;

        public SaleDetailsServiceController(SaleDetailsService saleDetailsService)
        {
            _saleDetailsService = saleDetailsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleDetails(SalesDetailsRequestModel model)
        {
            var result = await _saleDetailsService.CreateSaleDetails(model);
            return Execute(result);
        }
    }
}
