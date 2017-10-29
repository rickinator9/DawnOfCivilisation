using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IPlayer
    {
        ICountry Country { get; set; }
    }

    public class Player : IPlayer
    {
        public ICountry Country { get; set; }
    }
}