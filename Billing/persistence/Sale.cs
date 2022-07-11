using Billing.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Billing.persistence
{
    public class Sale : ISale
    {
        private readonly IBasket basket;
        private readonly List<IBookStock> books;
        private readonly List<IDeal> deals;
        private readonly IDictionary<string, decimal> BookList;

        public decimal Afterdiscount { get; set; }
        public List<IDealsinSale> DealsInSales { get; set; }

        /// <summary>
        /// Dependency Injection in sale
        /// </summary>
        /// <param name="basket">Adding Basket to Add or Remove Products</param>
        /// <param name="books">List of Books in Stock</param>
        /// <param name="deals">List of Deals for Sale</param>
        public Sale(IBasket basket, List<IBookStock> books, List<IDeal> deals)
        {
            this.basket = basket;
            this.books = books;
            this.deals = deals;   
            this.BookList = books.ToDictionary(x => x.SKU.ToUpper(), x => x.Price);
        }      

        /// <summary>
        /// Scanning Book for Basket
        /// </summary>
        /// <param name="book">Scanned Book</param>
        /// <returns>Self</returns>
        public void Scan(Book book)
        {
            this.basket.Add(book);            
        }

        /// <summary>
        /// Get List of books in Basket
        /// </summary>
        /// <returns></returns>
        public List<Book> ScannedBooks()
        {
            return this.basket.GetBasket();
        }

        /// <summary>
        /// Generate Sales Invoice for Customer
        /// </summary>
        /// <param name="path">Receipt File Path</param>
        /// <param name="name">Name of Customer</param>
        /// <param name="address">Address of Customer</param>
        public void CalculateSale(string path, string name, string address)
        {
            CalculateDiscount();
            var fs = File.OpenWrite(path);
            var x = Encoding.ASCII.GetBytes(string.Format("Date: {0}\r\nName: {1}\r\nAddress: {2}\r\n\r\n", DateTime.Now.ToShortDateString(), name, address));
            fs.Write(x, 0, x.Length);

            var x1 = "Description\t\t\t\tQuantity\t\tPrice\r\n";
            fs.Write(Encoding.ASCII.GetBytes(x1), 0, Encoding.ASCII.GetBytes(x1).Length);

            foreach(var basket in this.basket.GetBasket())
            {
                var s = string.Format($"{basket.sku.ToLowerInvariant()} Harry Potter\t\t\t{basket.Quantity}\t\t\t{basket.Quantity * BookList[basket.sku]}\r\n");
                fs.Write(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetBytes(s).Length);
            }
            var t = string.Format("\r\n");
            fs.Write(Encoding.ASCII.GetBytes(t), 0, Encoding.ASCII.GetBytes(t).Length);
            t = string.Format($"Total Before Discount:\t\t{this.Totalcost()}\r\n");
            fs.Write(Encoding.ASCII.GetBytes(t), 0, Encoding.ASCII.GetBytes(t).Length);
            foreach(var deal in DealsInSales)
            {
                var d = string.Format($"Discount {deal.deals.Name}\t\t {deal.discount}\r\n");
                fs.Write(Encoding.ASCII.GetBytes(d), 0, Encoding.ASCII.GetBytes(d).Length);
            }
            t = string.Format($"Total After Discount:\t\t{this.Afterdiscount}");
            fs.Write(Encoding.ASCII.GetBytes(t), 0, Encoding.ASCII.GetBytes(t).Length);
            fs.Close();
        }

        /// <summary>
        /// Calculate Discounts, finding Deals
        /// </summary>        
        public void CalculateDiscount()
        {
            Afterdiscount = 0m;            
            decimal totalbeforediscount = 0m;

            //collect deals applied in this sale
            DealsInSales = new List<IDealsinSale>(); 

            //Looping for set of quantity in the basket
            for (int i = MaxQuantity(); i > 0; i--)
            {
                //Getting set of items of the current qty
                var items = this.basket.GetBasket().Where(x => x.Quantity >= i).ToList();

                //Get deal of set
                IDeal deal = GetDeal(items.Count());
                if (deal.Discount == 0)
                {
                    Afterdiscount += (from item in items select BookList[item.sku]).Sum();
                }
                else
                {
                    //total before discount
                    totalbeforediscount = (from item in items select BookList[item.sku] * 1).Sum();
                    decimal totalafterdiscount = (from item in items
                            select CalculateDiscountPrice(BookList[item.sku], deal.Discount) * 1).Sum();
                    DealsInSales.Add(new DealsinSale() {deals= deal, Price= totalbeforediscount,PriceAfterDiscount = totalafterdiscount, discount= totalbeforediscount- totalafterdiscount });
                    Afterdiscount += totalafterdiscount;
                } 
            }            
        }

        /// <summary>
        /// Get Deal for the set found in sales
        /// </summary>
        /// <param name="Noofbooks">Set of Books</param>
        /// <returns>Deal</returns>
        private IDeal GetDeal(int Noofbooks)
        {
            return this.deals.SingleOrDefault(x => x.ForPurchaseOf == Noofbooks);
        }

        /// <summary>
        /// Get Discount Price
        /// </summary>
        /// <param name="price">Price of Book</param>
        /// <param name="discount">Discount Percentage</param>
        /// <returns>Price After Discount</returns>
        private decimal CalculateDiscountPrice(decimal price, decimal discount)
        {
            decimal discPrice = price - (price * discount / 100);
            return discPrice;
        }

        /// <summary>
        /// Mamimum Quantity of Basket
        /// </summary>
        /// <returns>Max Quantiyt</returns>
        private int MaxQuantity()
        {
            return this.basket.GetBasket().Select(x => x.Quantity).Max();            
        }

        /// <summary>
        /// Get Total Sale of Books without Discount 
        /// </summary>
        /// <returns></returns>
        private decimal Totalcost()
        {
            return this.basket.GetBasket().Select(x => x.Quantity * BookList[x.sku]).Sum();
        }        
    }
}
