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
        private ILogger Logger;

        public Order(OrderModel orderModel, IPromotionCalculator promotionCalculator, ILogger logger)
        {
            OrderModel = orderModel;
            PromotionCalculator = promotionCalculator;
            Logger = logger;
        }

        public void OrderDetails(IPromotionCalculator promotionCalculator)
        {
            try
            {
                List<OrderLineModel> orderLines = new List<OrderLineModel>();
                foreach (var line in OrderModel.SKU.GroupBy(info => info.ProductId)
                .Select(group => new
                {
                    product = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.product))
                {
                    orderLines.Add(new OrderLineModel() {  OrderLineId =1, ProductId = line.product, Quantity = line.Count });
                }
                //yet to implement, Validate order
                decimal promoprice;
                List<OrderPromo> promoprices = promotionCalculator.GetPromotionDetails(orderLines, out promoprice);

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
            catch(Exception e)
            {
                Logger.LogInfo(e.Message);
            }
        }

        public void PrintOrderDetails()
        {
            Console.WriteLine($"OrderID: {OrderModel.OrderId} => Original price: {OrderModel.OrderTotal.ToString("0.00")} | totalDiscount: {OrderModel.Discount.ToString("0.00")} | Final price: {(OrderModel.Discount + OrderModel.OrderDiscountedTotal).ToString("0.00")}");

        }
    }
}
