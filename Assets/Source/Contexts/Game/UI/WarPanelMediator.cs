using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarPanelMediator : ViewMediator<WarPanelView>
    {
        [Inject]
        public ShowUiPanelSignal ShowUiPanelDispatcher { get; set; }

        [Inject]
        public IPlayers Players { get; set; }

        private IWar _war;
        public IWar War
        {
            get { return _war; }
            set
            {
                _war = value;
                View.PopulateUI(Players.LocalPlayer.Country, _war);
            }
        }

        public override void OnRegister()
        {
            base.OnRegister();

            View.ClickSignal.AddListener(OnClick);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.ClickSignal.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            ShowUiPanelDispatcher.Dispatch(UiType.WarOverviewPanel, War);
        }
    }
}