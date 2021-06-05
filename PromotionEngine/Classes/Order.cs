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
            decimal promoprice;
            List<OrderPromo> promoprices = promotionCalculator.GetPromotionDetails(OrderModel, out promoprice);

            decimal origprice = OrderModel.SKU.Sum(x => x.UnitPrice);
            OrderModel.OrderTotal = OrderModel.SKU.Sum(x => x.UnitPrice);
            
            decimal residueprice = 0;
            foreach (var pp in promoprices)
            {
                char id = pp.ProdId;
                var residueprice1 = OrderModel.SKU.Where(x => x.ProductId == id);
                residueprice += residueprice1.FirstOrDefault().UnitPrice * pp.residueNumber;
            }
            OrderModel.OrderDiscountedTotal = residueprice;
            OrderModel.Discount = promoprice;
            PrintOrderDetails();
        }

        public void PrintOrderDetails()
        {
            Console.WriteLine($"OrderID: {OrderModel.OrderId} => Original price: {OrderModel.OrderTotal.ToString("0.00")} | totalDiscount: {OrderModel.Discount.ToString("0.00")} | Final price: {(OrderModel.Discount + OrderModel.OrderDiscountedTotal).ToString("0.00")}");

        }
    }
}
