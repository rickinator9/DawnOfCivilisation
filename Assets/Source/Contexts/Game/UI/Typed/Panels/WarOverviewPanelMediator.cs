using Assets.Source.Contexts.Game.Model.Political;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class WarOverviewPanelMediator : TypedUiMediator<WarOverviewPanelView, WarOverviewPanelView, IWar>
    {
        protected override UiType UiType
        {
            get { return UiType.WarOverviewPanel; }
        }

        protected override void ShowUiPanelForObject(IWar obj)
        {
            View.UpdateValues(obj);

            View.Show();
        }
    }
}