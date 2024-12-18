﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class ProductRespModel
    {
        public string ProductCode { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ProductCategoryCode { get; set; }

    }
}