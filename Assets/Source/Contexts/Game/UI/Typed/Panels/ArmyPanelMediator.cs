using Assets.Source.Contexts.Game.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class ArmyPanelMediator : TypedUiMediator<ArmyPanelView, IArmy>
    {
        protected override UiType UiType
        {
            get { return UiType.ArmyPanel; }
        }
    }
}