using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string ProductCategoryCode { get; set; } = null!;
}
