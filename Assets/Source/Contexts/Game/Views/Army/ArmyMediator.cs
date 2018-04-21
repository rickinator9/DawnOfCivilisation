using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Views.Army
{
    public class ArmyMediator : ChildMediator<IArmy, IArmyView, ArmyView>
    {
        private IArmy Army { get; set; }

        private bool NeedsRefresh { get; set; }

        public override void Initialise(IArmy army)
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
                View.Position = Army.Position;
                View.Color = Army.Country.Color;
                
                NeedsRefresh = false;
            }
        }

        public void Refresh()
        {
            NeedsRefresh = true;
        }
    }
}