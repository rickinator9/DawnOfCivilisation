using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Core.IoC;

namespace Assets.Source.Contexts.Game.UI
{
    public class BeligerentPanelMediator : ViewMediator<BeligerentPanelView>
    {
        private ICountry _country;

        public void Initialise(ICountry country)
        {
            _country = country;

            View.UpdateValues(country);
        }
    }
}