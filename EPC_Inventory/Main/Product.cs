﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPC_Inventory
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Stocks { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
    }
}
