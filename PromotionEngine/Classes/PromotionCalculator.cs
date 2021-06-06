using PromotionEngine.Interfaces;
using PromotionEngine.Model;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Classes
{
    public class PromotionCalculator : IPromotionCalculator
    {
        List<PromotionModel> Promotions;

        public PromotionCalculator(List<PromotionModel> promotions)
        {
            Promotions = promotions;
        }
        public List<OrderPromo> GetPromotionDetails(List<OrderLineModel> order, out decimal promvalue)
        {
            Dictionary<int, decimal> promApplied = new Dictionary<int, decimal>();
            Dictionary<int, int> promCount = new Dictionary<int, int>();

            //get count of promoted products in order
            foreach (PromotionModel prom in Promotions)
            {
                if (isValid(prom.PromotionID, order))
                {
                    // calculate values
                    int nofTimesPromapp = GetPromVlaues(prom.PromotionID, order);
                    promApplied.Add(prom.PromotionID, nofTimesPromapp * prom.Price);
                    //aggregate the values
                    //calculate residue
                }
            }
            promvalue = promApplied.Values.Sum();

            return  new List<OrderPromo>();

        }

        public bool isValid(int promID, List<OrderLineModel> order)
        {

            PromotionModel currPromotion = Promotions.Single(p => p.PromotionID == promID);
            bool validity = false;
            //get prom value and the prom count of products
            foreach (KeyValuePair<char, int> promProds in currPromotion.PromotionInfo)
            {
                validity = (order.Exists(w => w.ProductId == promProds.Key && w.Quantity >= promProds.Value)) ? true : false;
            }

            return validity;

        }

        public int GetPromVlaues(int promID, List<OrderLineModel> order)
        {
            Dictionary<int, decimal> currPromValueSet = new Dictionary<int, decimal>();

            PromotionModel validPromotion = Promotions.Single(x => x.PromotionID == promID);
            //set it to the max value
            int noofTimesApplicable = validPromotion.PromotionInfo.Values.Max();
            foreach (KeyValuePair<char, int> promProds in validPromotion.PromotionInfo)
            {
                if (order.Exists(x => x.ProductId == promProds.Key))
                {
                    OrderLineModel Ordeline = order.Single(x => x.ProductId == promProds.Key);
                    noofTimesApplicable = (Ordeline.Quantity / promProds.Value < noofTimesApplicable) ?
                                           Ordeline.Quantity / promProds.Value : noofTimesApplicable;
                }
            }
            return  noofTimesApplicable;
        }
    }
}
