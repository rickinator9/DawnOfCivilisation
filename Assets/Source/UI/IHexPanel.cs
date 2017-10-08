using Assets.Source.Model;
using UnityEditor;

namespace Assets.Source.UI
{
    public interface IHexPanel
    {
        void Show();
        void ShowForHexTile(IHexTile tile);

        void Hide();
    }
}