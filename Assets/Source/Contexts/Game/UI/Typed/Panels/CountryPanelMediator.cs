using Assets.Source.Contexts.Game.Commands.War;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CountryPanelMediator : TypedUiMediator<CountryPanelView, ICountry>
    {
        [Inject]
        public PlayerWarDeclarationSignal PlayerWarDeclarationDispatcher { get; set; }

        protected override UiType UiType
        {
            get { return UiType.CountryPanel; }
        }

        public override void OnRegister()
        {
            base.OnRegister();

            View.DeclareWarSignal.AddListener(OnDeclareWar);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.DeclareWarSignal.RemoveListener(OnDeclareWar);
        }

        private void OnDeclareWar(ICountry attackedCountry)
        {
            PlayerWarDeclarationDispatcher.Dispatch(attackedCountry);
        }
    }
}