using System;
using System.Collections.Generic;

namespace DotNetPOS.Database.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ProductCategoryCode { get; set; }

    public virtual ProductCategory? ProductCategoryCodeNavigation { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
