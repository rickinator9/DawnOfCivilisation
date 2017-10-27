using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class ArmyMediator : ViewMediator<ArmyView>
    {
        private IArmy Army { get; set; }

        private bool NeedsRefresh { get; set; }

        public void Initialise(IArmy army)
        {
            Army = army;

            var refreshSignal = new Signal();
            refreshSignal.AddListener(Refresh);
            Army.RefreshSignals.Add(refreshSignal);

            Refresh();
        }

        void Update()
        {
            if (NeedsRefresh)
            {
                View.OnRefresh(Army);
                NeedsRefresh = false;
            }
        }

        public void Refresh()
        {
            NeedsRefresh = true;
        }
    }
}