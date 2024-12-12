using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class SaleDetail
{
    public int SaleDetailsId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Product ProductCodeNavigation { get; set; } = null!;

    public virtual Sale VoucherNoNavigation { get; set; } = null!;
}
