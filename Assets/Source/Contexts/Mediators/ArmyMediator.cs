using Assets.Source.Contexts.Model;
using Assets.Source.Contexts.Views;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Mediators
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