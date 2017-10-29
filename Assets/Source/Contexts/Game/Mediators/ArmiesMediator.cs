using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
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