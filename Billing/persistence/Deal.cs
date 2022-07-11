using Billing.core;
using System;

namespace Billing.persistence
{
    public class Deal : IDeal
    {
        public string Name { get; private set; }
        public decimal Discount { get; private set; }
        public int ForPurchaseOf { get; private set; }       

        public Deal(string name, decimal discount, int forPurchase)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Nameless deal is not marketable");
            if (discount < 0)
                throw new ArgumentException("Discount cannot increase sales' price");
            if (forPurchase == 0)
                throw new ArgumentException("Must specify purchase items for which deal is given");
            this.Name = name;
            this.Discount = discount;
            this.ForPurchaseOf = forPurchase;
        }

        public override string ToString()
        {
            return string.Format("{0} -{1:C}", Name, Discount);
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Deal pi = obj as Deal;
            if ((System.Object)pi == null)
            {
                return false;
            }
            return this == pi;
        }

        public static bool operator ==(Deal a, Deal b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Discount == b.Discount && a.Name == b.Name && a.ForPurchaseOf == b.ForPurchaseOf;
        }

        public static bool operator !=(Deal a, Deal b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (int)Discount;
        }
    }
}
