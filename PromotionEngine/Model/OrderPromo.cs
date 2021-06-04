using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
    public class OrderPromo
    {
        public char PromId { get; set; }
        public int residueNumber { get; set; }
        public decimal AppliedPromotionValue {get; set;}
    }
}
