using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Political;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI
{
    public class WarPanelView : View
    {
        [SerializeField] private Text CountryText;

        public void PopulateUI(ICountry playerCountry, IWar war)
        {
            CountryText.text = war.GetEnemiesOfCountry(playerCountry)[0].Name;
        }
    }
}