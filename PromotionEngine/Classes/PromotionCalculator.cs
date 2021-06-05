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
        public List<OrderPromo> GetPromotionDetails(OrderModel orderModel, out decimal promvalue)
        {
            Dictionary<int, decimal> promApplied = new Dictionary<int, decimal>();
            Dictionary<int, int> promCount = new Dictionary<int, int>();

            List<OrderPromo> v = new List<OrderPromo>();
            foreach (var line in orderModel.SKU.GroupBy(info => info.ProductId)
                .Select(group => new
                {
                    product = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.product))
            {
                v.Add(new OrderPromo() { ProdId = line.product, residueNumber = line.Count });
            }

            //no of times promo is applied
            int PCount = 0;

            int pProd = 0;

            //get count of promoted products in order
            foreach (PromotionModel prom in Promotions)
            {
                //reset count for each promo
                pProd = 0;
                //get prom value and the prom count of products
                foreach (KeyValuePair<char, int> key in prom.PromotionInfo)
                {
                    if (v.Exists(w => w.ProdId == key.Key))
                    {
                        var pVal = v.Single(w => w.ProdId == key.Key);
                        pProd = pVal.residueNumber / key.Value;
                        if (pProd > 0 && pProd >= PCount)
                        {
                            PCount = pProd;
                        }
                        else
                        {
                            PCount = 0;
                        }
                    }
                    else
                    {
                        PCount = 0;
                    }

                }
                if (PCount > 0)
                {
                    // apply promotion value
                    promApplied.Add(prom.PromotionID, PCount * prom.Price);
                    promCount.Add(prom.PromotionID, PCount);
                }
            }
        
            //Update residue values, i.e remove products used for promotion codes
            foreach (KeyValuePair<int, int> key1 in promCount)
            {
                //find the promotion
                var prom1 = Promotions.Single(c => c.PromotionID == key1.Key).PromotionInfo;
                foreach(KeyValuePair<char, int> key2 in prom1)
                {
                    var vRec = v.Single(c => c.ProdId == key2.Key);
                    vRec.residueNumber = vRec.residueNumber / key2.Value == 0 ? 0 : vRec.residueNumber % key2.Value;

                }
            }
            promvalue = promApplied.Values.Sum();
            return v;

        }
    }
}
