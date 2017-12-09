using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI
{
    public class CountrySelectionMediator : ViewMediator<CountrySelectionView>
    {
        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.OnViewPressedSignal.AddListener(OnViewPressed);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.OnViewPressedSignal.RemoveListener(OnViewPressed);
        }

        private void OnViewPressed()
        {
            Players.LocalPlayer.SelectedObject = Players.LocalPlayer.Country;
            ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.CountryPanel, Players.LocalPlayer.Country);
        }
    }
}