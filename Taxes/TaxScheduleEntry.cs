using System;

namespace Taxes
{
    public class TaxScheduleEntry : ITaxGetter
    {
        public readonly DateRange dateRange;
        public readonly double taxRate;

        public TaxScheduleEntry(DateRange range, double rate)
        {
            dateRange = range;
            taxRate = rate;
        }

        public static bool IsDateInRange(DateTime date, DateRange range)
        {
            return range.startDate <= date && date <= range.endDate;
        }

        public double GetTax(DateTime dateOfQuery)
        {
            return IsDateInRange(dateOfQuery, dateRange) ? taxRate : 0.0;
        }
    }

    public class TaxScheduleEntryYearly : TaxScheduleEntry
    {
        public TaxScheduleEntryYearly(int year, double rate)
            : base(new DateRange(
                new DateTime(year, 1, 1),
                new DateTime(year, 12, 31)), rate)
        {
        }
    }

    public class TaxScheduleEntryMonthly : TaxScheduleEntry
    {
        public TaxScheduleEntryMonthly(int year, int month, double rate)
            : base(new DateRange(
                new DateTime(year, month, 1),
                new DateTime(year, month, DateTime.DaysInMonth(year, month))), rate)
        {
        }
    }

    public class TaxScheduleEntryWeekly : TaxScheduleEntry
    {
        public TaxScheduleEntryWeekly(int year, int month, int day, double rate)
            : base(new DateRange(
                new DateTime(year, month, day),
                new DateTime(year, month, day).AddDays(6)), rate)
        {
        }
    }

    public class TaxScheduleEntryDaily : TaxScheduleEntry
    {
        public TaxScheduleEntryDaily(int year, int month, int day, double rate)
            : base(new DateRange(
                new DateTime(year, month, day),
                new DateTime(year, month, day)), rate)
        {
        }
    }
}
