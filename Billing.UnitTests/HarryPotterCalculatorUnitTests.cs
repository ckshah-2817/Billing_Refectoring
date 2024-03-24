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

          
            if (File.Exists("Invoice_Buy5Books.txt"))
                File.Delete("Invoice_Buy5Books.txt");

            calculator.CalculateCost(book1, book2, book3, book4, book5, "Invoice_Buy5Books.txt", "Joe Customer", "London");
           
            Assert.AreEqual(136, File.OpenRead("Invoice_Buy5Books.txt").Length);



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

            if (File.Exists("Invoice_Buy7Books.txt"))
                File.Delete("Invoice_Buy7Books.txt");
            
             calculator.CalculateCost(book1, book2, book3, book4, book5, "Invoice_Buy7Books.txt", "Joe Customer", "London");
            
            Assert.AreEqual(165, File.OpenRead("Invoice_Buy7Books.txt").Length);

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

            if (File.Exists("Invoice_Buy8Books.txt"))
                File.Delete("Invoice_Buy8Books.txt");
            
              calculator.CalculateCost(book1, book2, book3, book4, book5, "Invoice_Buy8Books.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("Invoice_Buy8Books.txt").Length);
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
            if (File.Exists("InvoiceP1.txt"))
                File.Delete("InvoiceP1.txt");
            
            calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP1.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("InvoiceP1.txt").Length);
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
            if (File.Exists("InvoiceP2.txt"))
                File.Delete("InvoiceP2.txt");
            
               calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP2.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("InvoiceP2.txt").Length);
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
            if (File.Exists("InvoiceP3.txt"))
                File.Delete("InvoiceP3.txt");
            
            calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP3.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("InvoiceP3.txt").Length);
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
            if (File.Exists("InvoiceP4.txt"))
                File.Delete("InvoiceP4.txt");
           
             calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP4.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("InvoiceP4.txt").Length);
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
            if (File.Exists("InvoiceP5.txt"))
                File.Delete("InvoiceP5.txt");
            
            calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP5.txt", "Joe Customer", "London");
            
            Assert.AreEqual(136, File.OpenRead("InvoiceP5.txt").Length);
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
            if (File.Exists("InvoiceP6.txt"))
                File.Delete("InvoiceP6.txt");
            
             calculator.CalculateCost(book1, book2, book3, book4, book5, "InvoiceP6.txt", "Joe Customer", "London");
            
            Assert.AreEqual(165, File.OpenRead("InvoiceP6.txt").Length);
        }
    }
}