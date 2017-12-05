using Assets.Source.Contexts.Game.Model.Country;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI
{
    public class BeligerentPanelView : View
    {
        [SerializeField] private Text CountryNameText;

        public void UpdateValues(ICountry country)
        {
            CountryNameText.text = country.Name;
        }
    }
}