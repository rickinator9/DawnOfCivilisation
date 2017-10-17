using System.Collections.Generic;

namespace Assets.Source.Model.Impl
{
    public class DateManager : IDateManager
    {
        private static IDateManager _instance;
        public static IDateManager Instance
        {
            get { return _instance ?? (_instance = new DateManager()); }
        }

        private IDictionary<string, IDate> DatesByString { get; set; }

        private const int DaysPerMonth = 30;
        private const int MonthsPerYear = 12;
        private const int DaysPerYear = DaysPerMonth*MonthsPerYear;

        private DateManager()
        {
            DatesByString = new Dictionary<string, IDate>();
        }

        public IDate CurrentDate { get; private set; }

        public void Initialise(IDate startDate)
        {
            CurrentDate = startDate;
        }

        public IDate GetDate(byte day, byte month, short year)
        {
            return GetOrCreateDate(day, month, year);
        }

        public IDate AddDays(IDate date, int days)
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

            return GetOrCreateDate((byte)day, (byte)month, (short)year);
        }

        public IDate AddMonths(IDate date, int months)
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

            return GetOrCreateDate((byte)day, (byte)month, (byte)year);
        }

        public IDate AddYears(IDate date, short years)
        {
            return GetOrCreateDate(date.Day, date.Month, (short)(date.Year + years));
        }

        public IDate RemoveDays(IDate date, int days)
        {
            throw new System.NotImplementedException();
        }

        public IDate RemoveMonths(IDate date, int months)
        {
            throw new System.NotImplementedException();
        }

        public IDate RemoveYears(IDate date, short years)
        {
            return AddYears(date, (short)(years*-1));
        }

        public void OnTick()
        {
            CurrentDate = AddDays(CurrentDate, 1);
        }

        private IDate GetOrCreateDate(byte day, byte month, short year)
        {
            var date = new Date(day, month, year);
            var dateString = date.ToString();
            if (DatesByString.ContainsKey(dateString)) return DatesByString[dateString];

            DatesByString[dateString] = date;
            return date;
        }
    }
}