namespace Assets.Source.Model.Impl
{
    public class LocalPlayer : Player, ILocalPlayer
    {
        public ISelectable SelectedObject { get; set; }
    }
}