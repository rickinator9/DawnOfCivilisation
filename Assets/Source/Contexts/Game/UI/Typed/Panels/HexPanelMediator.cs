using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{

    public class HexPanelMediator : TypedUiMediator<HexPanelView, IHexTile>
    {
        [Inject]
        public CreateArmySignal CreateArmyDispatcher { get; set; }

        protected override UiType UiType
        {
            get { return UiType.HexPanel; }
        }

        public override void OnRegister()
        {
            base.OnRegister();

            View.RaiseArmySignal.AddListener(RaiseArmy);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.RaiseArmySignal.RemoveListener(RaiseArmy);
        }

        private void RaiseArmy(IHexTile tile)
        {
            CreateArmyDispatcher.Dispatch(tile);
        }
    }
}