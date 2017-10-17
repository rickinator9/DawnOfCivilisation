namespace Assets.Source.Model
{
    public delegate void DateCallback(IDate date);

    public interface IDate
    {
        byte Day { get; }

        byte Month { get; }

        short Year { get; } 
    }
}