using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarsPanelMediator : ViewMediator<WarsPanelView, WarsPanelView>
    {
        [Inject]
        public IPlayers Players { get; set; }

        public Signal<IPlayer> LocalPlayerChangeSignal = new Signal<IPlayer>();
        public Signal<IWar> WarAddedSignal = new Signal<IWar>(); 

        private IPlayer LocalPlayer { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            Players.LocalPlayerChangeSignals.Add(LocalPlayerChangeSignal);
            LocalPlayerChangeSignal.AddListener(OnLocalPlayerChange);

            WarAddedSignal.AddListener(OnWarAdded);
        }

        public override void OnRemove()
        {
            Players.LocalPlayerChangeSignals.Remove(LocalPlayerChangeSignal);
            LocalPlayerChangeSignal.RemoveListener(OnLocalPlayerChange);

            WarAddedSignal.RemoveListener(OnWarAdded);
        }

        private void OnLocalPlayerChange(IPlayer localPlayer)
        {
            if (LocalPlayer != null)
            {
                LocalPlayer.Country.Wars.OnAdd -= OnWarAdded;
            }
            LocalPlayer = localPlayer;
            if (LocalPlayer != null)
            {
                LocalPlayer.Country.Wars.OnAdd += OnWarAdded;
            }
        }

        private void OnWarAdded(IWar war)
        {
            View.CreateViewForObject(war);
        }
    }
}