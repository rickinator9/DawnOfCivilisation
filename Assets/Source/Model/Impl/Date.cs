namespace Assets.Source.Model.Impl
{
    public class Date : IDate
    {
        public byte Day { get; private set; }
        public byte Month { get; private set; }
        public short Year { get; private set; }

        public Date(byte day, byte month, short year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", Day, Month, Year);
        }
    }
}