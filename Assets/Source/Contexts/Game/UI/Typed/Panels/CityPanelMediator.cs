using Assets.Source.Contexts.Game.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CityPanelMediator : TypedUiMediator<CityPanelView, CityPanelView, ICity>
    {
        private ICity _city;

        protected override UiType UiType
        {
            get { return UiType.CityPanel; }
        }

        protected override void ShowUiPanelForObject(ICity obj)
        {
            _city = obj;
            if (_city == null) return;

            View.CityName = _city.Name;
            View.CountryName = _city.Country?.Name ?? "None";
            View.PopulationAmount = _city.Population;

            View.Show();
        }
    }
}