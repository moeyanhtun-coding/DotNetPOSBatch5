using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class SalesDetailsRequestModel
    {
        public List<ProductDetail> Products { get; set; } = new List<ProductDetail>();
    }

    public class ProductDetail
    {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }

}
