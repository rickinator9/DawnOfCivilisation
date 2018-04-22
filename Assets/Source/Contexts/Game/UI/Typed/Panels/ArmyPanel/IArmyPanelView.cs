using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.ArmyPanel
{
    public interface IArmyPanelView : IDOCView
    {
        string CountryName { set; }
    }
}
