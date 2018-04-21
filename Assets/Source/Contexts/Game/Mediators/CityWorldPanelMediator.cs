using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class CityWorldPanelMediator : ChildMediator<ICity, CityWorldPanelView, CityWorldPanelView>
    {
        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        private ICity City { get; set; }

        public override void Initialise(ICity city)
        {
            City = city;

            View.CityName = City.Name;
            View.transform.position += City.Location.Center;
        }

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
    }
}