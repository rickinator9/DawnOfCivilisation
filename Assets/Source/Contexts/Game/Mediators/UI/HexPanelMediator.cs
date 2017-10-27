using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Views.UI;
using Assets.Source.Core.IoC;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Mediators.UI
{
    public class HexPanelMediator : ViewMediator<HexPanelView>
    {
        [Inject]
        public CreateArmySignal CreateArmyDispatcher { get; set; }

        public override void OnRegister()
        {
            View.RaiseArmySignal.AddListener(RaiseArmy);
        }

        private void RaiseArmy(IHexTile tile)
        {
            CreateArmyDispatcher.Dispatch(tile);
        }
    }
}