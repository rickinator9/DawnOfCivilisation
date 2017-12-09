using Assets.Source.Contexts.Game.Model.Country;

namespace Assets.Source.Contexts.Game.Model.Player
{
    public interface IHumanPlayer : IPlayer
    {
         
    }

    public class HumanPlayer : IHumanPlayer
    {
        public ICountry Country { get; set; }
        public PlayerType Type { get { return PlayerType.Human; } }
    }
}