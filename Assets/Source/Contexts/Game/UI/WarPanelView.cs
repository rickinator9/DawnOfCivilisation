using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Political;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarPanelView : View
    {
        [SerializeField] private Text CountryText;

        public Signal ClickSignal = new Signal();

        public void PopulateUI(ICountry playerCountry, IWar war)
        {
            CountryText.text = war.GetEnemiesOfCountry(playerCountry)[0].Name;
        }

        public void OnClick()
        {
            ClickSignal.Dispatch();
        }
    }
}