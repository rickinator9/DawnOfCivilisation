using Assets.Source.Contexts.Model;
using Assets.Source.Contexts.Signals.Armies;
using Assets.Source.Contexts.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Mediators
{
    public class ArmiesMediator : ViewMediator<ArmiesView>
    {
        [Inject]
        public OnCreateArmySignal OnCreateArmyListener { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            OnCreateArmyListener.AddListener(OnCreateArmy);
        }

        private void OnCreateArmy(IArmy army)
        {
            View.CreateViewForArmy(army);
        }
    }
}