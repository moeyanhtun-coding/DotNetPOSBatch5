using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class TblSaleDetail
{
    public int SaleDetailId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual TblProduct ProductCodeNavigation { get; set; } = null!;

    public virtual TblSale VoucherNoNavigation { get; set; } = null!;
}
