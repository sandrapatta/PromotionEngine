using PromotionEngine.Model;
using System.Collections.Generic;

namespace PromotionEngine.Interfaces
{
    public interface IPromotionCalculator
    {
        public List<OrderPromo> GetPromotionDetails(List<OrderLineModel> order, out decimal promvalue);
        public bool isValid(int promID, List<OrderLineModel> order);
        public int GetPromVlaues(int promID, List<OrderLineModel> order);
    }
}
