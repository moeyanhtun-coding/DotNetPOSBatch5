namespace DotNetPOSBatch5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleController : Controller
{
    private readonly SaleService _saleService = new SaleService();
    private readonly SaleDetailsService _saleDetailsService = new SaleDetailsService();

    [HttpGet]
    public IActionResult GetSalesList()
    {
        var list = _saleService.GetSales();
        return Ok(list);
    }

    [HttpPost]
    public IActionResult CreateSale(SaleRequestModel reqMole)
    {
        var result = _saleService.CreateSale(reqMole);
        if (result is 0)
            return BadRequest(new { message = "Sale not created" });
        return Ok(new { message = "Sale created" });
    }
}