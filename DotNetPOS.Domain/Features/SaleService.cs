namespace DotNetPOS.Domain.Features;

public class SaleService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblSale> GetSales()
    {
        var lst = _db.TblSales.ToList();
        return lst;
    }
    
    public int CreateSale(SaleRequestModel reqModel)
    {
        var voucherNo = Ulid.NewUlid().ToString();
        _db.TblSaleDetails.Add(new TblSaleDetail
        {
            VoucherNo = voucherNo,
            ProductCode = reqModel.ProductCode,
            Quantity = reqModel.Quantity,
            Price = reqModel.Price,
        });

        _db.TblSales.Add(new TblSale
        {
            VoucherNo = voucherNo,
            SaleDate = DateTime.Now,
            TotalAmount = reqModel.Quantity * reqModel.Price,
        });
        int result = _db.SaveChanges();
        return result;
    }
}