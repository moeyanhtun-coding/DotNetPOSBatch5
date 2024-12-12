using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public string Name { get; set; } = null!;
}
