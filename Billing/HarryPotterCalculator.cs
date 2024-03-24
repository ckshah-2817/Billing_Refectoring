using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Billing
{
    public class HarryPotterCalculator
    {
        static Stack<int> s = new Stack<int>();

        public void CalculateCost(int book1, int book2, int book3, int book4, int book5, string path, string name, string address)
        {
            CalculateHarryPotter(book1, book2, book3, book4, book5, path, name, address);
        }

        private void CalculateHarryPotter(int book1, int book2, int book3, int book4, int book5, string path, string name, string address)
        {

            //Extracted file writing logic into a separate method WriteToFile for better modularity and readability.
            //Renamed variables for better clarity (quantity to books, totalCost instead of quantityCost, etc.).
            //Used using statement for file handling to ensure proper disposal.
            //Replaced minFee initialization with decimal.MaxValue for clarity.
            //Applied appropriate access modifiers to methods.
            //Removed redundant null parameter from string.Format.
            //We can also convert all method params to array but for now kept as it is.

            var costs = new HashSet<int[]>(new EqualityComparer());
            for (int quantity = 1; CanBuyHarryPotter(book1, book2, book3, book4, book5, quantity); ++quantity)
            {
                s.Push(quantity);
                costs.UnionWith(CalculateBuyHarryPotter(book1, book2, book3, book4, book5, quantity, 0m));
                s.Pop();
            }

            decimal minFee = decimal.MaxValue;
            int[] minBooks = null;
            foreach (var books in costs)
            {
                decimal totalCost = 0;
                for (var n = 1; n <= 5; ++n)
                {
                    decimal costPerBook = 8m;
                    decimal discount = GetDiscount(n); // refactor
                    totalCost += books[n] * n * costPerBook * (1m - discount);
                }

                if (totalCost < minFee)
                {
                    minFee = totalCost;
                    minBooks = books;
                }
                Debug.WriteLine("Books: {0} = {1}", string.Join(",", books), totalCost);
            }

            Debug.WriteLine("=============================");
            Debug.WriteLine("Books: {0} = {1}", string.Join(",", minBooks), minFee);
            Debug.WriteLine("=============================");

            WriteToFile(path, name, address, minBooks, minFee);
        }

        private void WriteToFile(string path, string name, string address, int[] minBooks, decimal minFee)
        {
            using (var fs = File.OpenWrite(path))
            {
                var header = Encoding.ASCII.GetBytes($"Date: {DateTime.Now.ToShortDateString()}\r\nName: {name}\r\nAddress: {address}\r\n\r\n");
                fs.Write(header, 0, header.Length);

                var description = "Description\t\t\t\t\tQuantity\tPrice\r\n";
                fs.Write(Encoding.ASCII.GetBytes(description), 0, Encoding.ASCII.GetBytes(description).Length);

                for (var n = 1; n <= 5; ++n)
                {
                    if (minBooks[n] > 0)
                    {
                        decimal discount = GetDiscount(n); // refactor
                        var line = string.Format("{0} Harry Potter books\t\t{1}\t\t\t{2}\r\n", n, minBooks[n], n * 8m * (1m - discount));
                        fs.Write(Encoding.ASCII.GetBytes(line), 0, Encoding.ASCII.GetBytes(line).Length);
                    }
                }

                var totalLine = string.Format("Total: {0}\r\n", minFee);
                fs.Write(Encoding.ASCII.GetBytes(totalLine), 0, Encoding.ASCII.GetBytes(totalLine).Length);
            }
        }




        class EqualityComparer : IEqualityComparer<int[]>
        {


            //Added null and length checks to the Equals method to ensure safe comparison.
            //Changed the loop in Equals method to iterate over the entire length of the arrays, not just up to index 5.
            //Modified the GetHashCode method to calculate the hash code in a more robust manner, ensuring it produces consistent results for equal arrays.Also, used unchecked context to prevent arithmetic overflow exceptions.
            public bool Equals(int[] x, int[] y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x == null || y == null || x.Length != y.Length) return false;

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                        return false;
                }

                return true;
            }

            public int GetHashCode(int[] obj)
            {
                unchecked
                {
                    int hash = 17;
                    foreach (int item in obj)
                    {
                        hash = hash * 23 + item.GetHashCode();
                    }
                    return hash;
                }
            }
        }

        private static HashSet<int[]> CalculateBuyHarryPotter(int book1, int book2, int book3, int book4, int book5, int n, decimal totalPrice)
        {

            //I've added comments to explain the purpose of each section of the code.
            //Variable names are kept consistent with the original code for clarity.
            //The calculation of discount is left as a call to GetDiscount(n) assuming that it's defined elsewhere in the codebase.
            //I retained the usage of s assuming it's a global or class-level variable. If it's not, you should pass it as a parameter to the method.
            // Get discount based on the number of different books purchased
            decimal discount = GetDiscount(n);

            // Cost per book
            decimal costPerBook = 8m;

            // Adjust total price with the cost of books including discount
            totalPrice += n * costPerBook * (1m - discount);

            // Adjust book quantities based on buying strategy
            BuyHarryPotter(ref book1, ref book2, ref book3, ref book4, ref book5, n);

            // If no books are purchased, return the quantities of each book
            if ((book1 + book2 + book3 + book4 + book5) == 0)
            {
                // Calculate book quantities
                int[] quantities = new int[6];
                s.ToArray().GroupBy(x => x).ToList().ForEach(x =>
                {
                    quantities[x.Key] = x.Count();
                });

                // Return quantities as a HashSet
                return new HashSet<int[]>(new EqualityComparer()) { quantities };
            }

            // HashSet to store costs of different buying strategies
            HashSet<int[]> costs = new HashSet<int[]>(new EqualityComparer());

            // Iterate through possible numbers of books to buy and calculate costs recursively
            for (int purchaseCount = 1; CanBuyHarryPotter(book1, book2, book3, book4, book5, purchaseCount); ++purchaseCount)
            {
                // Push the current purchase count onto a stack for tracking
                s.Push(purchaseCount);

                // Recursively calculate costs for different buying strategies
                costs.UnionWith(CalculateBuyHarryPotter(book1, book2, book3, book4, book5, purchaseCount, totalPrice));

                // Pop the current purchase count from the stack after processing
                s.Pop();
            }

            // Return the HashSet containing costs of different buying strategies
            return costs;
        }

        private static bool CanBuyHarryPotter(int book1, int book2, int book3, int book4, int book5, int N)
        {

            //Used an array to store the book counts, which simplifies the code.
            //Used a for loop to iterate over the books array instead of repeating similar code blocks.
            //Removed the unused conditional block.
            //Removed the goto statement for better code readability and maintainability.

            if (N > 5) return false;

            int[] books = { book1, book2, book3, book4, book5 };
            int countBought = 0;

            for (int i = 0; i < 5; i++)
            {
                if (books[i] > 0)
                {
                    --books[i];
                    ++countBought;
                }

                if (countBought == N)
                    return true;
            }

            return false;
        }

        private static void BuyHarryPotter(ref int book1, ref int book2, ref int book3, ref int book4, ref int book5, int N)
        {

            //Used LINQ to simplify the sorting process.
            //Utilized a single loop to iterate over the sorted books.
            //Removed unnecessary anonymous type fields.
            //Consolidated the error check condition to avoid redundancy.
            if (N == 0 || N > 5) return;

            var books = new[] { book1, book2, book3, book4, book5 };
            var sortedBooks = books.Select((count, index) => new { Count = count, Id = index + 1 })
                                   .OrderByDescending(b => b.Count)
                                   .ToArray();

            foreach (var book in sortedBooks.Take(N))
            {
                switch (book.Id)
                {
                    case 1:
                        --book1;
                        break;
                    case 2:
                        --book2;
                        break;
                    case 3:
                        --book3;
                        break;
                    case 4:
                        --book4;
                        break;
                    case 5:
                        --book5;
                        break;
                }

                if (book.Id >= 1 && book.Id <= 5 && books[book.Id - 1] < 0)
                {
                    throw new Exception();
                }
            }
        }


        private static decimal GetDiscount(int n)
        {
            switch (n)
            {
                case 2:
                    return 0.05m;
                case 3:
                    return 0.10m;
                case 4:
                    return 0.20m;
                case 5:
                    return 0.25m;
                default:
                    return 0m;
            }
        }
    }
}