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
        public List<ProductDetail> Products { get; set; } = new List<ProductDetail>();

        public decimal? TotalPrice { get; set; }

    }

}
