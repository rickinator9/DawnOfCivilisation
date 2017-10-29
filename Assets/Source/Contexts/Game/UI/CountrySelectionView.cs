using Assets.Source.Model.Impl;
using Assets.Source.UI.Controllers;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.UI
{
    public class CountrySelectionView : View
    {
        public Signal OnViewPressedSignal = new Signal();

        public void OnCountrySelected()
        {
            OnViewPressedSignal.Dispatch();
        }
    }
}