using Billing.core;

namespace Billing.persistence
{
    public class BookStock : IBookStock
    {
        public string SKU { get; set; }
        public decimal Price { get; set; }        
    }
}
