using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Classes
{
    public class PromotionCalculator : IPromotionCalculator
    {
        List<PromotionModel> Promotions;

        public PromotionCalculator(List<PromotionModel> promotions)
        {
            Promotions = promotions;
        }
        public List<OrderPromo> GetPromotionDetails(OrderModel orderModel)
        {
            decimal d = 0m;

            List<OrderPromo> v = new List<OrderPromo>();
            foreach (var line in orderModel.SKU.GroupBy(info => info.ProductId)
                .Select(group => new
                {
                    product = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.product))
            {
                v.Add(new OrderPromo() { PromId = line.product, residueNumber = line.Count });
            }
           
            //get count of promoted products in order
            foreach (PromotionModel prom in Promotions)
            {
                bool flagpromo = true;
                //update the residue value
                foreach (KeyValuePair<char, int> key in prom.PromotionInfo)
                {
                    foreach (var p in v.Where(w => w.PromId == key.Key))
                    {
                        if (flagpromo)
                        {
                            int ppc = p.residueNumber;
                            while (ppc >= key.Value)
                            {
                                d += prom.Price;
                                ppc -= key.Value;
                            }
                        }
                        p.AppliedPromotionValue = d;
                        //to overcome the remainer is ppc problem cos any p.residueNumber less than ppc will return ppc as remainder
                        p.residueNumber = p.residueNumber / key.Value == 0 ? 0 : p.residueNumber % key.Value;

                        //if a promotion is applied once it cannot be appled again (for any key that has mutiple products involved
                        flagpromo = false;
                        d = 0;

                    }
                  
                }
            }
            return v;


        }
    }
}
