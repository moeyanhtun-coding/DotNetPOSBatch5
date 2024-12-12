using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class TblSale
{
    public int SaleId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime SaleDate { get; set; }

    public string TotalAmount { get; set; } = null!;
}
