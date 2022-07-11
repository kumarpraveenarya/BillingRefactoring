using Billing.core;
using System;

namespace Billing.persistence
{
    public class Book : IBook
    {
        public string sku { get; set; }
        public int Quantity { get; set; }

        public Book(string sku, int quantity)
        {
            if (sku == null)
                throw new ArgumentNullException("SKU must be provided");
            if (quantity == 0)
                throw new ArgumentException("Cannot purchase zero units");
            this.sku = sku.ToUpper();
            this.Quantity = quantity;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Book pi = obj as Book;
            if ((System.Object)pi == null)
            {
                return false;
            }
            return this == pi;
        }

        public static bool operator ==(Book a, Book b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.sku == b.sku && a.Quantity == b.Quantity;
        }

        public static bool operator !=(Book a, Book b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (int)Quantity;
        }
    }
}
