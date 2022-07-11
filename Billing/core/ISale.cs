using Billing.persistence;
using System.Collections.Generic;

namespace Billing.core
{
    public interface ISale
    {
        List<IDealsinSale> DealsInSales { get; set; }
        decimal Afterdiscount { get; set; }

        void Scan(Book book);
        List<Book> ScannedBooks();
        void CalculateDiscount();
        void CalculateSale(string path, string name, string address);
    }
}
