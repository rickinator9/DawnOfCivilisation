using Assets.Source.Contexts.Model;
using Assets.Source.Model;

namespace Assets.Source.UI
{
    public interface IArmyPanel : IPanel
    {
        void ShowForArmy(IArmy army);
    }
}