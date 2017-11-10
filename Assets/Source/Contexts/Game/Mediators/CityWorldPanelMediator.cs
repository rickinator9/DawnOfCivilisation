using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class CityWorldPanelMediator : ViewMediator<CityWorldPanelView>
    {
        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        public ICity City { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.OnClickSignal.AddListener(OnViewClicked);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.OnClickSignal.RemoveListener(OnViewClicked);
        }

        private void OnViewClicked()
        {
            ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.CityPanel, City);
        }

        public void Initialise(ICity city)
        {
            City = city;
            View.UpdateUi(city);
        }
    }
}