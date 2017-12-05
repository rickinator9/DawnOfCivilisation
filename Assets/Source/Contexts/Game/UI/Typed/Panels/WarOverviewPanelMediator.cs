using Assets.Source.Contexts.Game.Model.Political;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class WarOverviewPanelMediator : TypedUiMediator<WarOverviewPanelView, IWar>
    {
        protected override UiType UiType
        {
            get { return UiType.WarOverviewPanel; }
        }
    }
}