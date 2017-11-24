using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IPlayer
    {
        ICountry Country { get; set; }
    }

    public class Player : IPlayer
    {
        private ICountry _country;
        public ICountry Country { get; set; }
    }
}