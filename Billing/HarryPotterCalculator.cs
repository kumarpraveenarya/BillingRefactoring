using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class HarryPotterCalculator
    {
        public void CalculateCost(int book1, int book2, int book3, int book4, int book5, string path, string name, string address)
        {
            CalculateHarryPotter(book1, book2, book3, book4, path, book5, name, address);
        }
        
        private void CalculateHarryPotter(int book1, int book2, int book3, int book4, string path, int book5, string name, string address)
        {
            HashSet<int[]> costs = new HashSet<int[]>(new EqualityComparer());
            for (int N = 1; CanBuyHarryPotter(book1, book2, book3, book4, book5, N); ++N)
            {
                s.Push(N);
                costs.UnionWith(CalculateBuyHarryPotter(book1, book2, book3, book4, book5, N, 0m));
                s.Pop();
            }

            decimal minFee = 999m;
            int[] minBooks = null;
            foreach (var quantity in costs)
            {
                decimal quantityCost = 0;
                for (var n = 1; n <= 5; ++n)
                {
                    decimal costPerBook = 8m;

                    decimal discount;
                    switch (n)
                    {
                        case 2:
                            discount = 0.05m;
                            break;
                        case 3:
                            discount = 0.10m;
                            break;
                        case 4:
                            discount = 0.20m;
                            break;
                        case 5:
                            discount = 0.25m;
                            break;
                        default:
                            discount = 0;
                            break;
                    }

                    quantityCost += quantity[n] * n * costPerBook * (1m - discount);
                }

                if (quantityCost < minFee)
                {
                    minFee = quantityCost;
                    minBooks = quantity;
                }
                Debug.WriteLine("Books: {0} = {1}", string.Join(",", quantity), quantityCost);
            }
            Debug.WriteLine("=============================");
            Debug.WriteLine("Books: {0} = {1}", string.Join(",", minBooks), minFee);
            Debug.WriteLine("=============================");

            var fs = File.OpenWrite(path);

            var x = Encoding.ASCII.GetBytes(string.Format("Date: {0}\r\nName: {1}\r\nAddress: {2}\r\n\r\n", DateTime.Now.ToShortDateString(), name, address));
            fs.Write(x, 0, x.Length);

            var x1 = "Description\t\t\t\t\tQuantity\tPrice\r\n";
            fs.Write(Encoding.ASCII.GetBytes(x1), 0, Encoding.ASCII.GetBytes(x1).Length);

            int i = 0;
            for(var n = 1; n <= 5; ++n)
            {
                if (minBooks[n] > 0)
                {
                    decimal discount;
                    switch (n)
                    {
                        case 2:
                            discount = 0.05m;
                            break;
                        case 3:
                            discount = 0.10m;
                            break;
                        case 4:
                            discount = 0.20m;
                            break;
                        case 5:
                            discount = 0.25m;
                            break;
                        default:
                            discount = 0;
                            break;
                    }

                    var s = string.Format("{0} Harry Potter books\t\t{1}\t\t\t{2}\r\n", n, minBooks[n], n * 8m * (1m - discount));
                    fs.Write(Encoding.ASCII.GetBytes(s), 0, Encoding.ASCII.GetBytes(s).Length);
                }
            }
            var s2 = string.Format("Total: {0}\r\n", minFee, null);
            fs.Write(Encoding.ASCII.GetBytes(s2), 0, Encoding.ASCII.GetBytes(s2).Length);

            fs.Close();
        }

        static Stack<int> s = new Stack<int>();

        class EqualityComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                for (int i = 1; i <= 5; ++i)
                {
                    if (x[i] != y[i]) return false;
                }
                return true;
            }

            public int GetHashCode(int[] obj)
            {
                return obj.Sum(x => x.GetHashCode());
            }
        }

        private static HashSet<int[]> CalculateBuyHarryPotter(int book1, int book2, int book3, int book4, int book5, int n, decimal f)
        {
            decimal discount;
            switch (n)
            {
                case 2:
                    discount = 0.05m;
                    break;
                case 3:
                    discount = 0.10m;
                    break;
                case 4:
                    discount = 0.20m;
                    break;
                case 5:
                    discount = 0.25m;
                    break;
                default:
                    discount = 0;
                    break;
            }

            decimal costPerBook = 8m;
            f += n * costPerBook * (1m - discount);
            BuyHarryPotter(ref book1, ref book2, ref book3, ref book4, ref book5, n);

            if ((book1 + book2 + book3 + book4 + book5) == 0)
            {
                int[] quantities = new int[6];
                s.ToArray().GroupBy(x => x).ToList().ForEach(x => {
                    quantities[x.Key] = x.Count();
                });

                return new HashSet<int[]>(new EqualityComparer()) { quantities };
            }

            HashSet<int[]> costs = new HashSet<int[]>(new EqualityComparer());
            for (int N = 1; CanBuyHarryPotter(book1, book2, book3, book4, book5, N); ++N)
            {
                s.Push(N);
                costs.UnionWith(CalculateBuyHarryPotter(book1, book2, book3, book4, book5, N, f));
                s.Pop();
            }

            return costs;
        }
        
        private static bool CanBuyHarryPotter(int book1, int book2, int book3, int book4, int book5, int N)
        {
            if (N > 5) return false;
            if (N == 4 && book1 != 0)
            {

            }

            int countBought = 0;
            if (book1 > 0)
            {
                --book1;
                ++countBought;
            }
            if (countBought == N) goto END;

            if (book2 > 0)
            {
                --book2;
                ++countBought;
            }
            if (countBought == N) goto END;

            if (book3 > 0)
            {
                --book3;
                ++countBought;
            }
            if (countBought == N) goto END;

            if (book4 > 0)
            {
                --book4;
                ++countBought;
            }
            if (countBought == N) goto END;

            if (book5 > 0)
            {
                --book5;
                ++countBought;
            }
            if (countBought == N) goto END;
            return false;

            END:
            return true;
        }

        private static void BuyHarryPotter(ref int book1, ref int book2, ref int book3, ref int book4, ref int book5, int N)
        {
            if (N == 0 || N > 5) return;

            var x = new[] { new { c = book1, id = 1 }, new { c = book2, id = 2 }, new { c = book3, id = 3 }, new { c = book4, id = 4 }, new { c = book5, id = 5 } }
                            .OrderByDescending(b => b.c).ToArray();
            foreach(var b in x.Take(N))
            {
                switch(b.id)
                {
                    case 1:
                        --book1;
                        if (book1 < 0)
                        {
                            throw new Exception();
                        }
                        break;
                    case 2:
                        --book2;
                        if (book2 < 0)
                        {
                            throw new Exception();
                        }
                        break;
                    case 3:
                        --book3;
                        if (book3 < 0)
                        {
                            throw new Exception();
                        }
                        break;
                    case 4:
                        --book4;
                        if (book4 < 0)
                        {
                            throw new Exception();
                        }
                        break;
                    case 5:
                        --book5;
                        if (book5 < 0)
                        {
                            throw new Exception();
                        }
                        break;
                }
            }
        }
    }
}
