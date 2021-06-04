namespace PromotionEngine.Model
{
    public class OrderLineModel
    {
        public int OrderLineId { get; set; }
        public decimal LineTotal { get; set; }
        public char ProductId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
