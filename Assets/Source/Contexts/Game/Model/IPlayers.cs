using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IPlayers
    {
        ILocalPlayer LocalPlayer { get; set; }
    }

    class Players : IPlayers
    {
        public ILocalPlayer LocalPlayer { get; set; }
    }
}