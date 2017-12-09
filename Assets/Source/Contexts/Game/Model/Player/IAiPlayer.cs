using Assets.Source.Contexts.Game.Model.Country;

namespace Assets.Source.Contexts.Game.Model.Player
{
    public interface IAiPlayer : IPlayer
    {
         
    }

    public class AiPlayer : IAiPlayer
    {
        public ICountry Country { get; set; }
        public PlayerType Type { get { return PlayerType.Ai; } }
    }
}