using System.Collections.Generic;

namespace PromotionEngine.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<ProductModel> SKU { get; set; }
        public List<char> ValidPromotions { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderDiscountedTotal { get; set; }

        public OrderModel(int id, List<ProductModel> prods)
        {
            this.OrderId = id;
            this.SKU = prods;
        }

    }
}
