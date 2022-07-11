using Billing.core;
using Billing.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class Merchant
    {
        public static void MakeSale()
        {
            List<IBookStock> Books = new List<IBookStock>();
            Books.AddRange(new BookStock[]
            {
                new BookStock(){SKU= "Book1",Price= 8m},
                new BookStock(){SKU= "Book2",Price= 8m},
                new BookStock(){SKU= "Book3",Price= 8m},
                new BookStock(){SKU= "Book4",Price= 8m},
                new BookStock(){SKU= "Book5",Price= 8m},
                new BookStock(){SKU= "Book6",Price= 10m},
            });

            List<IDeal> Deals = new List<IDeal>();
            Deals.AddRange(new Deal[]
            {
                new Deal("No Discount", 0m, 1),
                new Deal("5% Discount", 5m, 2),
                new Deal("10% Discount", 5m, 3),
                new Deal("20% Discount", 5m, 4),
                new Deal("25% Discount", 5m, 5)
            });

            var sale = new Sale(new Basket(), Books, Deals);

            sale.Scan(new Book("Book1", 1));
            sale.Scan(new Book("Book2", 2));

            sale.CalculateSale("invoice.txt","Parveen Kumar", "Test Address of Parveen Kumar");            
        }
    }
}
