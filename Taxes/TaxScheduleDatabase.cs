using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Taxes
{
    public class TaxScheduleDatabase
    {
        public TaxScheduleDatabase()
        {
            scheduleDatabase = new Dictionary<string, TaxSchedule>();
        }

        public void LoadFromFile(string fullPath)
        {
            var lines = File.ReadAllLines(fullPath).Where(s => !string.IsNullOrWhiteSpace(s));
            foreach (var line in lines)
                ParseLine(line);
        }

        private void ParseLine(string line)
        {
            var entries = line.Split(' ');
            var city = entries[0];
            var mode = entries[1].ToLowerInvariant() ;
            if (mode == "yearly")
            {
                var year = entries[2];
                var rate = entries[3];
                var taxEntry = new TaxScheduleEntryYearly(int.Parse(year), double.Parse(rate));
                AddTaxToCity(city, taxEntry);
            }
            else if (mode == "monthly")
            {
                var year = entries[2];
                var month = entries[3];
                var rate = entries[4];
                var taxEntry = new TaxScheduleEntryMonthly(int.Parse(year), int.Parse(month), double.Parse(rate));
                AddTaxToCity(city, taxEntry);
            }
            //week starting with the specified day
            else if (mode == "weekly")
            {
                var year = entries[2];
                var month = entries[3];
                var day = entries[4];
                var rate = entries[5];
                var taxEntry = new TaxScheduleEntryWeekly(int.Parse(year), int.Parse(month), int.Parse(day), double.Parse(rate));
            }
            else if (mode == "daily")
            {
                var year = entries[2];
                var month = entries[3];
                var day = entries[4];
                var rate = entries[5];
                var taxEntry = new TaxScheduleEntryDaily(int.Parse(year), int.Parse(month), int.Parse(day), double.Parse(rate));
            }
            else
                throw new Exception(string.Format("Unrecognized mode {0}: Expecting Yearly, Monthly, Weekly or Daily\n Original line: {1}", mode, line));
        }

        public void AddTaxToCity(string city, TaxScheduleEntry scheduleEntry)
        {
            if (scheduleDatabase.ContainsKey(city))
            {
                //existing city, add entry to existing schedule
                var schedule = scheduleDatabase[city];
                schedule.AddEntry(scheduleEntry);
            }
            else
            {
                //new city, new schedule, with one entry
                var schedule = new TaxSchedule(city);
                schedule.AddEntry(scheduleEntry);
                scheduleDatabase.Add(city, schedule);
            }
        }

        public double GetTaxForCity(string city, DateTime dateTime)
        {
            TaxSchedule cityTaxSchedule;
            
            cityTaxSchedule = scheduleDatabase[city];
            
            return cityTaxSchedule.GetTax(dateTime);
        }
        Dictionary<string, TaxSchedule> scheduleDatabase;

       
    }
}
