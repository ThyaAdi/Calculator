using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Calculator.CheckBook;

namespace CalculatorTests
{
    //******************************Test1***************************************//
    [TestClass]
    public class LinqPractice
    {
        [TestMethod]
        public void AverageFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var total = ob.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Avg = g.Average(t => t.Amount) });

            Assert.AreEqual(32.625, total.First().Avg);
            Assert.AreEqual(75, total.Last().Avg);

        }
        //********************************Test2******************************//
        [TestMethod]
        public void TotalForEachPayee()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var Sum = ob.Transactions.GroupBy(t => t.Payee).Select(g => new { g.Key, Sum = g.Sum(t => t.Amount) }).ToArray();

            Assert.AreEqual(130, Sum[0].Sum);
            Assert.AreEqual(300, Sum[1].Sum);
            Assert.AreEqual(131, Sum[2].Sum);
        }
        //****************************test3**********************************//
        [TestMethod]
        public void PayeeForFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var FoodMoshe = ob.Transactions.Where(t => t.Tag == "Food").Where(t => t.Payee == "Moshe");
            var TotalMoshe = FoodMoshe.Sum(t => t.Amount);
            var FoodTim = ob.Transactions.Where(t => t.Tag == "Food").Where(t => t.Payee == "Tim");
            var TotalTim = FoodTim.Sum(t => t.Amount);
            var FoodBracha = ob.Transactions.Where(t => t.Tag == "Food").Where(t => t.Payee == "Bracha");
            var TotalBracha = FoodBracha.Sum(t => t.Amount);

            Assert.AreEqual(130, TotalMoshe);
            Assert.AreEqual(0, TotalTim);
            Assert.AreEqual(131, TotalBracha);   
        }
        //***********************Test4******************************************//
       [TestMethod]
        public void TransactionsBT()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            //var Date = ob.Transactions.Where(t => t.Date == "DateTime.Now.AddDays(-1)" && t.Date == "DateTime.Now.AddDays(-3)")
            //var match = from t in ob.Transactions where(t.Date).select();
            //Console.WriteLine(match);
            var Date = new DateTime(2015, 04, 06);
            var Date1 = ob.Transactions.Where(t => t.Date == Date).Select(t => t.Date);

            Assert.AreEqual(Date, Date1.ElementAt(0));
            Assert.AreEqual(Date, Date1.ElementAt(0));
         }
        //******************************Test5***************************************//
       [TestMethod]
       public void TransactionsDates()
       {
           var ob = new CheckBookVM();
           ob.Fill();

           var TransDates1 = ob.Transactions.Where(t => t.Account == "Checking").ToArray();
           Assert.AreEqual(new DateTime(2015,04,08), TransDates1[0].Date);
           Assert.AreEqual(new DateTime(2015, 04, 06), TransDates1[1].Date);
           Assert.AreEqual(new DateTime(2015, 04, 07), TransDates1[2].Date);
           Assert.AreEqual(new DateTime(2015, 04, 04), TransDates1[3].Date);
           Assert.AreEqual(new DateTime(2015, 04, 03), TransDates1[4].Date);
           Assert.AreEqual(new DateTime(2015, 04, 07), TransDates1[5].Date);

           var TransDates2 = ob.Transactions.Where(t => t.Account == "Credit").ToArray();
           Assert.AreEqual(new DateTime(2015, 04, 08), TransDates2[0].Date);
           Assert.AreEqual(new DateTime(2015, 04, 07), TransDates2[1].Date);
           Assert.AreEqual(new DateTime(2015, 04, 06), TransDates2[2].Date);
           Assert.AreEqual(new DateTime(2015, 04, 05), TransDates2[3].Date);
           Assert.AreEqual(new DateTime(2015, 04, 04), TransDates2[4].Date);
           Assert.AreEqual(new DateTime(2015, 04, 03), TransDates2[5].Date);

       }

        //**************************Test6*************************************//
        [TestMethod]
        public void AccountOnAuto()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var count = ob.Transactions.Where(t => t.Tag == "Auto").Select(t => t.Account);
            var count1 = ob.Transactions.Select(g => g.Amount).Max();
            Assert.AreEqual(130, count1);
            Assert.AreEqual("credit", count);
        }

        /*[TestMethod]
        public void AccountOnAuto()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            //var count = ob.Transactions.Where(t => t.Tag == "Auto").Where(g => g.Amount == g.Amount.Max().Select(t => t.Account));
            //var count1 = ob.Transactions.Select(g => g.Account).Max();
           // Assert.AreEqual("Credit", count);
        }*/

        //******************************Test7*******************************//
        [TestMethod]
        public void NumbersOfTransactionsOfEachAccount()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var Date = new DateTime(2015, 04, 06);
            var Date1 = ob.Transactions.Where(t => t.Date == Date).Select(t => t.Date).ToArray().Length;

            Assert.AreEqual(2, Date1);
        }

    }
}
