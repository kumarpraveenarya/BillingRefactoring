using Billing.core;
using Billing.persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Billing.UnitTests
{
    [TestClass]
    public class BooksSaleTests
    {
        private readonly ISale sale;               
               
        public BooksSaleTests()
        {
            IEnumerable<IBookStock> Books = new[]
            {
                new BookStock() { SKU = "Book1",Price = 8m},
                new BookStock() { SKU = "Book2",Price = 8m},
                new BookStock() { SKU = "Book3",Price = 8m},
                new BookStock() { SKU = "Book4",Price = 8m},
                new BookStock() { SKU = "Book5",Price = 8m},
                new BookStock() { SKU = "Book6",Price = 10m}
            };

            IEnumerable<IDeal> Deals = new[]
            {
                new Deal("No Discount", 0m, 1),
                new Deal("5% Discount", 5m, 2),
                new Deal("10% Discount", 10m, 3),
                new Deal("20% Discount", 20m, 4),
                new Deal("25% Discount", 25m, 5)
            };   
            sale = new Sale(new Basket(), Books.ToList(), Deals.ToList());
        }

        [TestMethod]
        public void ScanProductToSaleAdd1ProductinBasket()
        {
            sale.Scan(new Book("Book1", 1));
            int count = sale.ScannedBooks().Count();
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWith1Setof5BooksWithOneDeal25()
        {
            sale.Scan(new Book("Book1", 1));
            sale.Scan(new Book("Book2", 1));
            sale.Scan(new Book("Book3", 1));
            sale.Scan(new Book("Book4", 1));
            sale.Scan(new Book("Book5", 1));
            sale.CalculateDiscount();
            Assert.AreEqual(30m, sale.Afterdiscount);
            Assert.AreEqual(sale.DealsInSales.Count(), 1);
            Assert.AreEqual(sale.DealsInSales[0].deals.Discount, 25m);
        }


        [TestMethod]
        public void CalculateDiscountedMoneyWith2Setof2Books()
        {
            sale.Scan(new Book("Book1", 2));
            sale.Scan(new Book("Book2", 2));
            sale.CalculateDiscount();
            Assert.AreEqual(30.4m, sale.Afterdiscount);
            Assert.AreEqual(sale.DealsInSales.Count(), 2);
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWith2Setof2BooksGot2Deals()
        {
            sale.Scan(new Book("Book1", 2));
            sale.Scan(new Book("Book2", 2));
            sale.CalculateDiscount();           
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWith2Setof3Books()
        {
            sale.Scan(new Book("Book1", 2));
            sale.Scan(new Book("Book2", 2));
            sale.Scan(new Book("Book3", 2));
            sale.CalculateDiscount();
            Assert.AreEqual(43.2m, sale.Afterdiscount);
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWith2Setof3BooksGot2Deals()
        {
            sale.Scan(new Book("Book1", 2));
            sale.Scan(new Book("Book2", 2));
            sale.Scan(new Book("Book3", 2));
            sale.CalculateDiscount();
            Assert.AreEqual(2, sale.DealsInSales.Count());
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWithDifferentPriceBookAndQuantity()
        {
            sale.Scan(new Book("Book1", 3));
            sale.Scan(new Book("Book2", 2));
            sale.Scan(new Book("Book3", 1));
            sale.Scan(new Book("Book6", 1));
            sale.CalculateDiscount();
            Assert.AreEqual(50.4m, sale.Afterdiscount);
        }

        [TestMethod]
        public void CalculateDiscountedMoneyWithDifferentPriceBookAndQuantityGot5N20Deals()
        {
            sale.Scan(new Book("Book1", 3));
            sale.Scan(new Book("Book2", 2));
            sale.Scan(new Book("Book3", 1));
            sale.Scan(new Book("Book6", 1));
            sale.CalculateDiscount();
            Assert.AreEqual(2, sale.DealsInSales.Count());
        }

        [TestMethod]
        public void IsSaleGeneratingNewInvoice()
        {
            sale.Scan(new Book("Book1", 3));
            sale.Scan(new Book("Book2", 2));
            sale.Scan(new Book("Book3", 1));
            sale.Scan(new Book("Book6", 1));
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            sale.CalculateSale("Invoice.txt", "New Customer", "London");
            Assert.IsTrue(File.Exists("Invoice.txt"));

            Assert.AreEqual(318, File.OpenRead("Invoice.txt").Length);
        }
    }
}
