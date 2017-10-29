using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CountryPanelMediator : TypedUiMediator<CountryPanelView, ICountry>
    {
        protected override UiType UiType
        {
            get { return UiType.CountryPanel; }
        }
    }
}