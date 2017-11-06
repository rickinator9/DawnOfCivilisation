using Assets.Source.Contexts.Game.Commands.City;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class CityWorldPanelsMediator : ViewMediator<CityWorldPanelsView>
    {
         [Inject]
         public OnCreateCitySignal OnCreateCitySignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            OnCreateCitySignal.AddListener(OnCreateCity);
        }

        private void OnCreateCity(ICity obj)
        {
            View.CreateViewForObject(obj);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            OnCreateCitySignal.RemoveListener(OnCreateCity);
        }
    }
}