﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Order> OrderList { get; set; }
        public List<StateTax> TaxInfo { get; set; }
        public List<Product> ProductInfo { get; set; }

    }
}
