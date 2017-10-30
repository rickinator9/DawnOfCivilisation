namespace Assets.Source.Contexts.Game.Model
{
    public interface ICity
    {
        string Name { get; set; }

        int Population { get; }

        ICountry Country { get; }
    }
}