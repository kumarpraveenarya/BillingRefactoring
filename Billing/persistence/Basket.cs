using Billing.core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.persistence
{   
    public class Basket : IBasket
    {       
        private List<Book> basketItems { get; set; }         
         
        public Basket(){
            this.basketItems = new List<Book>();
        }
       
        public Basket(IEnumerable<Book> purchasedBooks)
        {
            if (purchasedBooks == null || purchasedBooks.Count() == 0)
                throw new ArgumentException("Need to provide purchase items for a basket");
            if (!purchasedBooks.All(item => item.sku != null))
                throw new ArgumentException("SKU cannot be null");
            this.basketItems = purchasedBooks.ToList();           
        }

        /// <summary>
        /// Adding new Book to Basket
        /// </summary>
        /// <param name="item">New Book</param>       
        public void Add(Book item)
        {
            this.basketItems.Add(item);    
        }

        /// <summary>
        /// Removing Book from Basket
        /// </summary>
        /// <param name="item">book to remove</param>        
        public void Remove(Book item)
        {            
            if (!Contains(item))
                throw new InvalidOperationException("Item to remove should be in basket");
            this.basketItems.Remove(item);            
        }        
        
        /// <summary>
        /// to check items exist in basket or not
        /// </summary>
        /// <param name="books">List of Books to check</param>
        /// <returns>true or false</returns>
        public bool Contains(IEnumerable<Book> books)
        {
            if (books == null || books.Count() == 0)
                throw new ArgumentException("Purchase items to check must be provided");
            return books.All(Contains);
        }

        /// <summary>
        /// to check book exist in basket or not
        /// </summary>
        /// <param name="book">book to check</param>
        /// <returns>true or false</returns>
        public bool Contains(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("Purchase item to check cannot be null");
            return basketItems.Contains(book);
        }

        /// <summary>
        /// Return Update Basket
        /// </summary>
        /// <returns></returns>
        public List<Book> GetBasket()
        {
            return this.basketItems.GroupBy(x => x.sku.ToUpper()).Select(x => new Book(x.Key, x.Sum(t => t.Quantity))).ToList();
        }
    }
}
