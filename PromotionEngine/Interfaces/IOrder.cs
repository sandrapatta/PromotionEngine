using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    public interface IOrder
    {
        public void PrintOrderDetails();
        public void OrderDetails(IPromotionCalculator promotionCalculator);

    }
} 
