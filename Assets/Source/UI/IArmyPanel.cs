using Assets.Source.Model;

namespace Assets.Source.UI
{
    public interface IArmyPanel
    {
        void Show();

        void ShowForArmy(IArmy army);

        void Hide();
    }
}