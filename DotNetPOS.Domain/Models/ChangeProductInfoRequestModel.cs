using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class ChangeProductInfoRequestModel
    {
        public string? Name { get; set; }

        public decimal? Price { get; set; }
    }
}
