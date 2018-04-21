using Assets.Source.Contexts.Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CityPanelView : TypedUiView<ICity>
    {
        [SerializeField]
        private Text _cityNameText;

        [SerializeField]
        private Text _countryNameText;

        [SerializeField]
        private Text _populationAmountText;

        public string CityName
        {
            get { return _cityNameText.text; }
            set { _cityNameText.text = value; }
        }

        public string CountryName
        {
            get { return _countryNameText.text; }
            set { _countryNameText.text = value; }
        }

        public int PopulationAmount
        {
            get { return int.Parse(_populationAmountText.text); }
            set { _populationAmountText.text = value.ToString(); }
        }
    }
}