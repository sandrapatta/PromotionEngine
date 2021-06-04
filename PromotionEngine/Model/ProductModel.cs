using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
    public class ProductModel
    {
        public char ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Name { get; set; }

        public ProductModel(char pId, decimal price, string name)
        {
            ProductId = pId;
            UnitPrice = price;
            Name = name;
        }

        public ProductModel(char pId)
        {
            ProductId = pId;
        }
    }
}
