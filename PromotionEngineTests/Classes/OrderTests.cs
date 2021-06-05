using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using PromotionEngine.ProductModule;
using System.Collections.Generic;
using Moq;

namespace PromotionEngine.Classes.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void OrderDetailsTest()
        {
            Dictionary<char, int> d1 = new Dictionary<char, int>();
            d1.Add('A', 3);
            Dictionary<char, int> d2 = new Dictionary<char, int>();
            d2.Add('B', 2);
            Dictionary<char, int> d3 = new Dictionary<char, int>();
            d3.Add('C', 1);
            d3.Add('D', 1);
            List<PromotionModel> promotions = new List<PromotionModel>()
            {
                new PromotionModel(1, d1, 130),
                new PromotionModel(2, d2, 45),
                new PromotionModel(3, d3, 30)
            };

            //create products
            ProductModel A = new ProductModel('A', 50, "Aprod");
            ProductModel B = new ProductModel('B', 30, "Aprod");
            ProductModel C = new ProductModel('C', 20, "Aprod");
            ProductModel D = new ProductModel('D', 15, "Aprod");


            OrderModel order1 = new OrderModel(1, new List<ProductModel>() { A, B, C });
            ILogger Logger = new ConsoleLogger();
            decimal outvalue = 0;
            Mock<IPromotionCalculator> calculator = new Mock<IPromotionCalculator>(MockBehavior.Strict);
            List<OrderPromo> orRes = new List<OrderPromo>();
            orRes.Add(new OrderPromo());
            calculator.Setup(s => s.GetPromotionDetails(order1, out outvalue)).Returns(orRes);
            IOrder order11 = new Order(order1, calculator.Object, Logger);
            Assert.IsTrue(orRes.Count > 0);
        }

        public void PrintOrderTest()
        {

        }
    }
}