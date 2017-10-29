using Assets.Source.Contexts.Game.Model;
using Assets.Source.UI.Controllers;

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