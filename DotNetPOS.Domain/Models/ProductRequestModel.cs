﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class ProductRequestModel
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string ProductCategoryCode { get; set; }
    }
}
