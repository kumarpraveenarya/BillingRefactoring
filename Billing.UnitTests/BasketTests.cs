using Billing.core;
using Billing.persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Billing.UnitTests
{
    [TestClass]
    public class BasketTests
    {
        private readonly IBasket basket;

        public BasketTests()
        {
            basket = new Basket(new Book[] { new Book("Book1", 1) });
        }

        [TestMethod]
        public void CheckBasketAfterInitializeShouldHave1Book()
        {
            int count = this.basket.GetBasket().Count();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void CheckBasketAfterInitializeShouldHaveSKUUpperCase()
        {
            var sku = this.basket.GetBasket().Select(x => x.sku).First();
            string result = sku.ToUpper();
            Assert.AreEqual(sku, result);
        }

        [TestMethod]
        public void CheckBasketAfterAdding1MoreBookShouldHave2Books()
        {
            basket.Add(new Book("Book2", 1));
            int count = this.basket.GetBasket().Count();
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void CheckBasketRemovingBookHave1Books()
        {
            basket.Remove(new Book("Book1", 1));
            int count = this.basket.GetBasket().Count();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CheckBasketContainsBook()
        {
            Assert.IsTrue(basket.Contains(new Book("Book1", 1)));   
        }

        [TestMethod]
        public void CheckBasketDoesNotContainsBook()
        {
            Assert.IsFalse(basket.Contains(new Book("Book2", 1)));
        }
    }
}
