using Assets.Source.Contexts.Game.Model.Hex;

namespace Assets.Source.Model
{
    public interface ISelectable
    {
        void OnRightClickOnTile(IHexTile tile);
    }
}