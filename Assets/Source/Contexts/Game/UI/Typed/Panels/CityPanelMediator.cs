using Assets.Source.Contexts.Game.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CityPanelMediator : TypedUiMediator<CityPanelView, ICity>
    {
        protected override UiType UiType
        {
            get { return UiType.CityPanel; }
        }
    }
}