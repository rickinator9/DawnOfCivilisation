using Assets.Source.Model;
using UnityEditor;

namespace Assets.Source.UI
{
    public interface IHexPanel : IPanel
    {
        void ShowForHexTile(IHexTile tile);
    }
}