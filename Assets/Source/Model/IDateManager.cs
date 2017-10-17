namespace Assets.Source.Model
{
    public interface IDateManager
    {
        IDate CurrentDate { get; }

        void Initialise(IDate startDate);

        IDate GetDate(byte day, byte month, short year);

        IDate AddDays(IDate date, int days);

        IDate AddMonths(IDate date, int months);

        IDate AddYears(IDate date, short years);

        IDate RemoveDays(IDate date, int days);

        IDate RemoveMonths(IDate date, int months);

        IDate RemoveYears(IDate date, short years);

        void OnTick();
    }
}