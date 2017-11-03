using System.Collections.Generic;

namespace Assets.Source.Model
{
    public struct TentativeDate
    {
        public byte Day;
        public byte Month;
        public short Year;
    }

    public interface IDateManager
    {
        IDate this[TentativeDate tentativeDate] { get; set; }
        IDate this[byte day, byte month, short year] { get; set; }
        IDate this[string date] { get; set; }

        IDate CurrentDate { get; set; }

        TentativeDate AddDays(IDate date, int days);

        TentativeDate AddMonths(IDate date, int months);

        TentativeDate AddYears(IDate date, short years);

        TentativeDate RemoveDays(IDate date, int days);

        TentativeDate RemoveMonths(IDate date, int months);

        TentativeDate RemoveYears(IDate date, short years);
    }

    public class DateManager : IDateManager
    {
        private IDictionary<string, IDate> DatesByString { get; set; }

        private const int DaysPerMonth = 30;
        private const int MonthsPerYear = 12;
        private const int DaysPerYear = DaysPerMonth * MonthsPerYear;

        public DateManager()
        {
            DatesByString = new Dictionary<string, IDate>();
        }

        public IDate this[TentativeDate tentativeDate]
        {
            get
            {
                var dateString = GetDateString(tentativeDate.Day, tentativeDate.Month, tentativeDate.Year);
                return GetDateByString(dateString);
            }
            set
            {
                var dateString = GetDateString(tentativeDate.Day, tentativeDate.Month, tentativeDate.Year);
                SetDateByString(dateString, value);
            }
        }

        IDate IDateManager.this[byte day, byte month, short year]
        {
            get
            {
                var dateString = GetDateString(day, month, year);
                return GetDateByString(dateString);
            }
            set
            {
                var dateString = GetDateString(day, month, year);
                SetDateByString(dateString, value);
            }
        }

        IDate IDateManager.this[string date]
        {
            get { return GetDateByString(date); }
            set { SetDateByString(date, value); }
        }

        public IDate CurrentDate { get; set; }

        public TentativeDate AddDays(IDate date, int days)
        {
            int day = date.Day + days;
            int month = date.Month;
            int year = date.Year;

            while (day > DaysPerYear)
            {
                year++;
                day -= DaysPerYear;
            }
            while (day > DaysPerMonth)
            {
                month++;
                day -= DaysPerMonth;
            }
            if (month > MonthsPerYear)
            {
                year++;
                month -= MonthsPerYear;
            }

            return new TentativeDate
            {
                Day = (byte)day,
                Month = (byte)month,
                Year = (short)year
            };
        }

        public TentativeDate AddMonths(IDate date, int months)
        {
            int day = date.Day;
            int month = date.Month;
            int year = date.Year;

            while (months > 12)
            {
                year++;
                months -= 12;
            }
            month += months;
            if (month > 12)
            {
                year++;
                month -= 12;
            }

            return new TentativeDate
            {
                Day = (byte)day,
                Month = (byte)month,
                Year = (short)year
            };
        }

        public TentativeDate AddYears(IDate date, short years)
        {
            return new TentativeDate
            {
                Day = date.Day,
                Month = date.Month,
                Year = (short)(date.Year + years)
            };
        }

        public TentativeDate RemoveDays(IDate date, int days)
        {
            throw new System.NotImplementedException();
        }

        public TentativeDate RemoveMonths(IDate date, int months)
        {
            throw new System.NotImplementedException();
        }

        public TentativeDate RemoveYears(IDate date, short years)
        {
            return AddYears(date, (short)(years * -1));
        }

        private IDate GetDateByString(string dateString)
        {
            return DatesByString.ContainsKey(dateString) ? DatesByString[dateString] : null;
        }

        private void SetDateByString(string dateString, IDate date)
        {
            DatesByString[dateString] = date;
        }

        private string GetDateString(byte day, byte month, short year)
        {
            return string.Format("{0}.{1}.{2}", day, month, year);
        }
    }
}