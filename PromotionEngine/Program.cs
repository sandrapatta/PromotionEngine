using PromotionEngine.Classes;
using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using PromotionEngine.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
             //current promotions
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

            //create Orders
            List<OrderModel> orders = new List<OrderModel>();

            OrderModel order1 = new OrderModel(1, new List<ProductModel>() { A,B, C });
            OrderModel order2 = new OrderModel(2, new List<ProductModel>() { A, A, A, A, A, B,B,B,B,B,C });
            OrderModel order3 = new OrderModel(3, new List<ProductModel>() { A, A, A, B, B, B, B, B, C, D, });
           
            IPromotionCalculator calculator = new PromotionCalculator(promotions);
            IOrder order11 = new Order(order1, calculator);
            IOrder order21 = new Order(order2, calculator);
            IOrder order31 = new Order(order3, calculator);
            List<IOrder> orders1 = new List<IOrder>() { order11, order21, order31 };

            //check if order meets promotion
            foreach (Order ord in orders1)
            {
                ord.OrderDetails(calculator);
            }
        }
    }
}

