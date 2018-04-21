using Assets.Source.Contexts.Game.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.ArmyPanel
{
    public class ArmyPanelMediator : TypedUiMediator<IArmyPanelView, ArmyPanelView, IArmy>
    {
        private IArmy _army;

        protected override UiType UiType
        {
            get { return UiType.ArmyPanel; }
        }

        protected override void ShowUiPanelForObject(IArmy obj)
        {
            _army = obj;
            if (_army == null) return;

            View.CountryName = _army.Country.Name;

            View.Show();
        }
    }
}