namespace DotNetPOS.Domain.Features;

public class SaleDetailsService
{
    private readonly AppDbContext _db = new AppDbContext();
    public List<TblSaleDetail> GetSaleDetails()
    {
        var lists = _db.TblSaleDetails.ToList()!;
        return lists;
    }

    public TblSaleDetail? GetSaleDetailByVoucherNo(string voucherNo)
    {
        var saleDetail = _db.TblSaleDetails.FirstOrDefault(s => s.VoucherNo == voucherNo);
        if (saleDetail is null)
            return null;
        return saleDetail;
    }
}