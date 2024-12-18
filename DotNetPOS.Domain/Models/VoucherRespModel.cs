using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class VoucherRespModel
    {
        public string VoucherNo { get; set; }
        public List<VoucherItemDetails> Products { get; set; } = new List<VoucherItemDetails>();

        public decimal? TotalPrice { get; set; }

    }

    public class VoucherItemDetails : ProductDetail
    {
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
