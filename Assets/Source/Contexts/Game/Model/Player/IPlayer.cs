using Assets.Source.Contexts.Game.Model.Country;

namespace Assets.Source.Contexts.Game.Model.Player
{
    public enum PlayerType
    {
        Human,
        Ai
    }

    public interface IPlayer
    {
        ICountry Country { get; set; }

        PlayerType Type { get; }
    }
}