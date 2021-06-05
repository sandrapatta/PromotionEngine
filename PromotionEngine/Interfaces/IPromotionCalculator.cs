using PromotionEngine.Model;
using System.Collections.Generic;

namespace PromotionEngine.Interfaces
{
    public interface IPromotionCalculator
    {
        public List<OrderPromo> GetPromotionDetails(OrderModel orderModel,out decimal promvalue);
    }
}
