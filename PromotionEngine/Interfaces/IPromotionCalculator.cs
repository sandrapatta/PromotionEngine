using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    public interface IPromotionCalculator
    {
        public List<OrderPromo> GetPromotionDetails(OrderModel orderModel);
    }
}
