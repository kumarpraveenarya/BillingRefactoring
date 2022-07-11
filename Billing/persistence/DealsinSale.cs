using Billing.core;

namespace Billing.persistence
{
    public class DealsinSale : IDealsinSale
    {
        public IDeal deals { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
