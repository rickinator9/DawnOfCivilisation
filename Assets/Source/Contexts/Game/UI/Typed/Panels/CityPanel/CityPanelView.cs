using Assets.Source.Contexts.Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.CityPanel
{
    public class CityPanelView : TypedUiView<ICity>, ICityPanelView
    {
        #region Unity Components
        [SerializeField] private Text _cityNameText;

        [SerializeField] private Text _countryNameText;

        [SerializeField] private Text _populationAmountText;
        #endregion

        public string CityName
        {
            set { _cityNameText.text = value; }
        }

        public string CountryName
        {
            set { _countryNameText.text = value; }
        }

        public int PopulationAmount
        {
            set { _populationAmountText.text = value.ToString(); }
        }
    }
}