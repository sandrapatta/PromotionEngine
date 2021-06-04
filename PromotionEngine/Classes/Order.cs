using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.ProductModule
{
    public class Order : IOrder
    {
        private OrderModel OrderModel;
        private IPromotionCalculator PromotionCalculator;

        public Order(OrderModel orderModel, IPromotionCalculator promotionCalculator)
        {
            OrderModel = orderModel;
            PromotionCalculator = promotionCalculator;
        }

        public void OrderDetails(IPromotionCalculator promotionCalculator)
        {
            List<OrderPromo> promoprices = promotionCalculator.GetPromotionDetails(OrderModel);

            decimal origprice = OrderModel.SKU.Sum(x => x.UnitPrice);
            decimal promoprice = promoprices.Sum(x=>x.AppliedPromotionValue);
            decimal residueprice = 0;
            foreach (var pp in promoprices)
                {
                  char  id = pp.PromId;
                  var residueprice1 = OrderModel.SKU.Where(x => x.ProductId == id);
                residueprice += residueprice1.FirstOrDefault().UnitPrice * pp.residueNumber;
                }
 

            Console.WriteLine($"OrderID: {OrderModel.OrderId} => Original price: {origprice.ToString("0.00")} | Rebate: {promoprice.ToString("0.00")} | Final price: {(promoprice + residueprice ).ToString("0.00")}");
        }

        public void PrintOrderDetails()
        {
            throw new NotImplementedException();
        }
    }
}
