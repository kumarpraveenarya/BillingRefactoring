using Billing.persistence;
using System.Collections.Generic;

namespace Billing.core
{
    public interface IBasket
    {        
        void Add(Book book);
        void Remove(Book item);        
        List<Book> GetBasket();
        bool Contains(IEnumerable<Book> book);
        bool Contains(Book book);          
    }
}
