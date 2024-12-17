using DotNetPOS.Domain.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleServiceController : BaseController
    {
        private readonly SaleService _saleService;

        public SaleServiceController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("{voucherNo}")]
        public async Task<IActionResult> GenerateVoucher(string voucherNo)
        {
            var result = await _saleService.GenerateVoucher(voucherNo);
            return Execute(result);
        }
    }
}
