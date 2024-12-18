using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleDetailController : Controller
{
    private readonly SaleDetailsService _saleDetailsService = new();
    
    [HttpGet]
    public IActionResult GetSaleDetails()
    {
        var lst = _saleDetailsService.GetSaleDetails();
        return Ok(lst);
    }

    [HttpGet("{voucherNo}")]
    public IActionResult GetSaleDetailByVoucherNo(string voucherNo)
    {
        var saleDetail = _saleDetailsService.GetSaleDetailByVoucherNo(voucherNo);
        return Ok(saleDetail);
    }
}