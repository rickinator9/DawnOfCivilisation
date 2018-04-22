using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;
using System.Collections.Generic;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class ArmiesMediator : ViewMediator<ArmiesView, ArmiesView>
    {
        [Inject]
        public OnCreateArmySignal OnCreateArmyListener { get; set; }

        private IList<IArmy> _armiesToCreate = new List<IArmy>();

        public override void OnRegister()
        {
            base.OnRegister();

            OnCreateArmyListener.AddListener(OnCreateArmy);
        }

        protected void Update()
        {
            for(int i = _armiesToCreate.Count-1 ; i >= 0 ; i--) {
                IArmy army = _armiesToCreate[i];

                View.CreateViewForObject(army);

                _armiesToCreate.RemoveAt(i);
            }
        }

        private void OnCreateArmy(IArmy army)
        {
            _armiesToCreate.Add(army);
        }
    }
}