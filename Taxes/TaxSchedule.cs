using System;
using System.Collections.Generic;

namespace Taxes
{
    public struct DateRange
    {
        public DateRange(DateTime start, DateTime end)
        {
            startDate = start;
            endDate = end;
        }

        public long TotalDays()
        {
            return (int)endDate.Subtract(startDate).TotalDays;
        }

        public readonly DateTime startDate;
        public readonly DateTime endDate;
    }

    public enum TaxScheduleKind
    {
        Yearly, Monthly, Weekly, Daily
    };

    internal class TaxSchedule : ITaxGetter
    {
        string cityName;
        internal TaxSchedule(string city)
        {
            cityName = city;
        } 

        //here we make the tax calculation based on the narrowest range of dates from the list
        //tax could also be done by adding the rates of all the date ranges that match 
        public double GetTax(DateTime queryDate)
        {
            DateRange narrowestRange = new DateRange(new DateTime(1, 1, 1), new DateTime(9999, 12, 31));
            TaxScheduleEntry entryWithNarrowestRange = null;
           
            foreach (var entry in scheduleEntryList)
            {
                if (TaxScheduleEntry.IsDateInRange(queryDate, entry.dateRange))
                {
                    if (entry.dateRange.TotalDays() < narrowestRange.TotalDays())
                    {
                        narrowestRange = entry.dateRange;
                        entryWithNarrowestRange = entry;
                    }
                }
            }
            return entryWithNarrowestRange== null? 0.0: entryWithNarrowestRange.GetTax(queryDate);
        }

        public void AddEntry(TaxScheduleEntry entry)
        {
            scheduleEntryList.Add(entry);
        }

        List<TaxScheduleEntry> scheduleEntryList = new List<TaxScheduleEntry>();

    }
}
