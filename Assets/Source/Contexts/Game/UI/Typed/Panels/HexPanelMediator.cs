using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{

    public class HexPanelMediator : TypedUiMediator<HexPanelView, IHexTile>
    {
        [Inject]
        public CreatePlayerArmySignal CreatePlayerArmyDispatcher { get; set; }

        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        protected override UiType UiType
        {
            get { return UiType.HexPanel; }
        }

        public override void OnRegister()
        {
            base.OnRegister();

            View.RaiseArmySignal.AddListener(RaiseArmy);
            View.ShowCountryPanelSignal.AddListener(ShowCountryPanel);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.RaiseArmySignal.RemoveListener(RaiseArmy);
            View.ShowCountryPanelSignal.RemoveListener(ShowCountryPanel);
        }

        private void RaiseArmy(IHexTile tile)
        {
            CreatePlayerArmyDispatcher.Dispatch(tile);
        }

        private void ShowCountryPanel(ICountry country)
        {
            ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.CountryPanel, country);
        }
    }
}