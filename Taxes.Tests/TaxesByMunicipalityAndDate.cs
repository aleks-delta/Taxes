using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taxes.Tests
{
    [TestClass]
    public class TaxesByMunicipalityAndDate
    {
        private TaxSchedule SetupVilniusSchedule()
        {
            var schedule = new TaxSchedule("Vilnius");
            
            schedule.AddEntry(new TaxScheduleEntryYearly(2016, 0.2));
            schedule.AddEntry(new TaxScheduleEntryMonthly(2016, 5, 0.4));
            schedule.AddEntry(new TaxScheduleEntryDaily(2016, 1, 1, 0.1));
            schedule.AddEntry(new TaxScheduleEntryDaily(2016, 12, 25, 0.1));

            return schedule;
        }

        [TestMethod]
        public void Taxes_VilniusJan1()
        {
            var schedule = SetupVilniusSchedule();
            var actualTax = schedule.GetTax(new DateTime(2016, 1, 1));
            Assert.AreEqual(0.1, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusMay2()
        {
            var schedule = SetupVilniusSchedule();
            var actualTax = schedule.GetTax(new DateTime(2016, 5, 2));
            Assert.AreEqual(0.4, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusJuly10()
        {
            var schedule = SetupVilniusSchedule();
            var actualTax = schedule.GetTax(new DateTime(2016, 7, 10));
            Assert.AreEqual(0.2, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusMar16()
        {
            var schedule = SetupVilniusSchedule();
            var actualTax = schedule.GetTax(new DateTime(2016, 3, 16));
            Assert.AreEqual(0.2, actualTax);
        }

       

    }
}
