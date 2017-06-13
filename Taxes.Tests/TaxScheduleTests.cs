using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taxes.Tests
{
    [TestClass]
    public class TaxScheduleTests
    {
        private TaxSchedule CreateVilniusYearlyTaxSchedule()
        {
            var entry = new TaxScheduleEntryYearly(2016, 0.1);
            var schedule = new TaxSchedule("Vilnius");
            schedule.AddEntry(entry);
            return schedule;
        }

        private TaxSchedule CreateVilniusMonthlyTaxSchedule()
        {
            var entry = new TaxScheduleEntryMonthly(2016, 5, 0.4);
            var schedule = new TaxSchedule("Vilnius");
            schedule.AddEntry(entry);
            return schedule;
        }

        private TaxSchedule CreateVilnius1stWeekOfSummerTaxSchedule()
        {
            var entry = new TaxScheduleEntryWeekly(2016, 6, 1, 0.7);
            var schedule = new TaxSchedule("Vilnius");
            schedule.AddEntry(entry);
            return schedule;
        }

        private TaxSchedule CreateVilniusChristmasTaxSchedule()
        {
            var entry = new TaxScheduleEntryDaily(2016, 12, 25, 0.1);
            var schedule = new TaxSchedule("Vilnius");
            schedule.AddEntry(entry);
            return schedule;
        }

        [TestMethod]
        public void YearlyTaxAlone_OneDay_YearlyTax()
        {
            var schedule = CreateVilniusYearlyTaxSchedule();
            var dayOfTax = new DateTime(2016, 2, 2);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.1, actualTax);
        }

        [TestMethod]
        public void YearlyTaxAlone_OneDayOutsideRange_ZeroTax()
        {
            var schedule = CreateVilniusYearlyTaxSchedule();
            var dayOfTax = new DateTime(2015, 2, 2);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.0, actualTax);
        }

        [TestMethod]
        public void MonthlyTaxAlone_OneDay_MonthlyTax()
        {
            var schedule = CreateVilniusMonthlyTaxSchedule();
            var dayOfTax = new DateTime(2016, 5, 2);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.4, actualTax);
        }


        [TestMethod]
        public void MonthlyTaxAlone_OneDayAnotherMonth_ZeroTax()
        {
            var schedule = CreateVilniusMonthlyTaxSchedule();
            var dayOfTax = new DateTime(2016, 4, 2);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.0, actualTax);
        }

        [TestMethod]
        public void ChristmasTaxAlone_ChristmasDay_ChristmasTax()
        {
            var schedule = CreateVilniusChristmasTaxSchedule();
            var dayOfTax = new DateTime(2016, 12, 25);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.1, actualTax);
        }

        [TestMethod]
        public void ChristmasTaxAlone_NotChristmasDay_NoTax()
        {
            var schedule = CreateVilniusChristmasTaxSchedule();
            var dayOfTax = new DateTime(2016, 2, 2);
            var actualTax = schedule.GetTax(dayOfTax);
            Assert.AreEqual(0.0, actualTax);
        }

        [TestMethod]
        public void FirstWeekOfSummer_HotSummerDay_SummerTax()
        {
            var schedule = CreateVilnius1stWeekOfSummerTaxSchedule();
            {
                var dayOfTax = new DateTime(2016, 6, 1);
                var actualTax = schedule.GetTax(dayOfTax);
                Assert.AreEqual(0.7, actualTax);
            }

            {
                var dayOfTax = new DateTime(2016, 6, 7);
                var actualTax = schedule.GetTax(dayOfTax);
                Assert.AreEqual(0.7, actualTax);
            }
        }

        [TestMethod]
        public void FirstWeekOfSummer_NotHotSummerDay_NoTax()
        {
            var schedule = CreateVilnius1stWeekOfSummerTaxSchedule();
            {
                var dayOfTax = new DateTime(2016, 5, 31);
                var actualTax = schedule.GetTax(dayOfTax);
                Assert.AreEqual(0.0, actualTax);
            }
            {
                var dayOfTax = new DateTime(2016, 6, 8);
                var actualTax = schedule.GetTax(dayOfTax);
                Assert.AreEqual(0.0, actualTax);
            }
        }
    }
}
