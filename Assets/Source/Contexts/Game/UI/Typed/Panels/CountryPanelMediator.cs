using Assets.Source.Contexts.Game.Commands.War;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CountryPanelMediator : TypedUiMediator<CountryPanelView, CountryPanelView, ICountry>
    {
        [Inject]
        public PlayerWarDeclarationSignal PlayerWarDeclarationDispatcher { get; set; }

        private ICountry _country;

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

        protected override void ShowUiPanelForObject(ICountry obj)
        {
            _country = obj;
            if (_country == null) return;

            View.CountryName = obj.Name;
            View.CanDeclareWar = !obj.IsHumanControlled;

            View.Show();
        }

        private void OnDeclareWar()
        {
            if(_country != null) PlayerWarDeclarationDispatcher.Dispatch(_country);
        }
    }
}