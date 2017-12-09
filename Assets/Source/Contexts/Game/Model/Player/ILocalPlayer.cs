namespace Assets.Source.Contexts.Game.Model.Player
{
    public interface ILocalPlayer : IHumanPlayer
    {
        ISelectable SelectedObject { get; set; }
    }

    public class LocalPlayer : HumanPlayer, ILocalPlayer
    {
        public ISelectable SelectedObject { get; set; }
    }
}