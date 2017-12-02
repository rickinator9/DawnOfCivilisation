using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarPanelMediator : ViewMediator<WarPanelView>
    {
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
    }
}