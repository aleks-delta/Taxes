using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taxes.Tests
{
    [TestClass]
    public class TaxesByMunicipalityAndDate
    {
        TaxScheduleDatabase db;
        public TaxesByMunicipalityAndDate()
        {
            db = new TaxScheduleDatabase();
        }

        private void SetupVilniusSchedule()
        {
            db.AddTaxToCity("Vilnius", new TaxScheduleEntryYearly(2016, 0.2));
            db.AddTaxToCity("Vilnius", new TaxScheduleEntryMonthly(2016, 5, 0.4));
            db.AddTaxToCity("Vilnius", new TaxScheduleEntryDaily(2016, 1, 1, 0.1));
            db.AddTaxToCity("Vilnius", new TaxScheduleEntryDaily(2016, 12, 25, 0.1));
        }

        [TestMethod]
        public void Taxes_VilniusJan1()
        {
            SetupVilniusSchedule();
            var actualTax = db.GetTaxForCity("Vilnius", new DateTime(2016, 1, 1));
            Assert.AreEqual(0.1, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusMay2()
        {
            SetupVilniusSchedule();
            var actualTax = db.GetTaxForCity("Vilnius", new DateTime(2016, 5, 2));
            Assert.AreEqual(0.4, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusJuly10()
        {
            SetupVilniusSchedule();
            var actualTax = db.GetTaxForCity("Vilnius", new DateTime(2016, 7, 10));
            Assert.AreEqual(0.2, actualTax);
        }

        [TestMethod]
        public void Taxes_VilniusMar16()
        {
            SetupVilniusSchedule();
            var actualTax = db.GetTaxForCity("Vilnius", new DateTime(2016, 3, 16));
            Assert.AreEqual(0.2, actualTax);
        }
    }
}
