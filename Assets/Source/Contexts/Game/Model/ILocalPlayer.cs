namespace Assets.Source.Contexts.Game.Model
{
    public interface ILocalPlayer : IPlayer
    {
        ISelectable SelectedObject { get; set; }
    }

    public class LocalPlayer : Player, ILocalPlayer
    {
        public ISelectable SelectedObject { get; set; }
    }
}