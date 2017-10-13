namespace Assets.Source.Model
{
    public interface ILocalPlayer : IPlayer
    {
        ISelectable SelectedObject { get; set; }
    }
}