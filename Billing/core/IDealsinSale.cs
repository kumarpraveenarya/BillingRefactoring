using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.core
{
    public interface IDealsinSale
    {
        IDeal deals { get; set; }
        decimal Price { get; set; }
        decimal discount { get; set; }
        decimal PriceAfterDiscount { get; set; }
    }
}
