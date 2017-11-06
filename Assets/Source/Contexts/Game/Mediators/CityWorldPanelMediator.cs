using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.Mediators
{
    public class CityWorldPanelMediator : ViewMediator<CityWorldPanelView>
    {
        public void Initialise(ICity city)
        {
            View.UpdateUi(city);
        }
    }
}