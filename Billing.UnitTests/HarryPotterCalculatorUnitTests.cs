using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing;
using System.IO;

namespace Billing.UnitTests
{
    [TestClass]
    public class HarryPotterCalculatorUnitTests
    {
        [TestMethod]
        public void Buy5Books()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 1;
            var book3 = 1;
            var book4 = 1;
            var book5 = 1;

            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);

        }

        [TestMethod]
        public void Buy7Books()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 2;
            var book2 = 3;
            var book3 = 2;
            var book4 = 0;
            var book5 = 2;

            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(165, File.OpenRead("Invoice.txt").Length);
            
        }

        [TestMethod]
        public void Buy8Books()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 2;
            var book2 = 2;
            var book3 = 2;
            var book4 = 1;
            var book5 = 1;

            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy8BooksP1()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 2;
            var book3 = 2;
            var book4 = 2;
            var book5 = 1;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy8BooksP2()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 2;
            var book3 = 2;
            var book4 = 1;
            var book5 = 2;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy8BooksP3()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 2;
            var book3 = 1;
            var book4 = 2;
            var book5 = 2;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy8BooksP4()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 2;
            var book2 = 1;
            var book3 = 1;
            var book4 = 2;
            var book5 = 2;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy8BooksP5()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 1;
            var book3 = 2;
            var book4 = 2;
            var book5 = 2;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(136, File.OpenRead("Invoice.txt").Length);
        }

        [TestMethod]
        public void Buy9BooksP1()
        {
            var calculator = new HarryPotterCalculator();
            var book1 = 1;
            var book2 = 2;
            var book3 = 3;
            var book4 = 2;
            var book5 = 1;
            if (File.Exists("Invoice.txt"))
                File.Delete("Invoice.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "invoice.txt", "Joe Customer", "London");

            Assert.AreEqual(165, File.OpenRead("Invoice.txt").Length);
        }
    }
}
