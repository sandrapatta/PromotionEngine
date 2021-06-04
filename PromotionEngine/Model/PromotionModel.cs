using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
   public class PromotionModel
    {
        public int PromotionID { get; set; }

        public Dictionary<char, int> PromotionInfo { get; set; }
        public char ProdCode { get; set; }
        public int ProdQuantity { get; set; }
        public decimal Price { get; set; }
         

        public PromotionModel(int id, Dictionary<char, int> promInfo, decimal price)
        {
            PromotionID = id;
            PromotionInfo = promInfo;
            Price = price;

        }
    }
}
