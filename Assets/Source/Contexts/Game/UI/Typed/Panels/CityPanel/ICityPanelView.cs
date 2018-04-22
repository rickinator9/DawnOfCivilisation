using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.CityPanel
{
    public interface ICityPanelView : IDOCView
    {
        string CityName { set; }
        string CountryName { set; }
        int PopulationAmount { set; }
    }
}
