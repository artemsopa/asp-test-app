using System;
using System.Collections.Generic;
using System.Text;

namespace TestAPI.Logic.Models
{
    public class Product_ResultSet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
