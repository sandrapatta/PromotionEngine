using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            OrderModel.OrderTotal = OrderModel.SKU.Sum(x => x.UnitPrice);
            decimal promoprice = promoprices.Sum(x => x.AppliedPromotionValue);
           
            OrderModel.Discount = promoprices.Sum(x => x.AppliedPromotionValue);
            decimal residueprice = 0;
            foreach (var pp in promoprices)
            {
                char id = pp.PromId;
                var residueprice1 = OrderModel.SKU.Where(x => x.ProductId == id);
                residueprice += residueprice1.FirstOrDefault().UnitPrice * pp.residueNumber;
            }
            OrderModel.OrderDiscountedTotal = residueprice;
            PrintOrderDetails();
        }

        public void PrintOrderDetails()
        {
            Console.WriteLine($"OrderID: {OrderModel.OrderId} => Original price: {OrderModel.OrderTotal.ToString("0.00")} | totalDiscount: {OrderModel.Discount.ToString("0.00")} | Final price: {(OrderModel.Discount + OrderModel.OrderDiscountedTotal).ToString("0.00")}");

        }
    }
}
