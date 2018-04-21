using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Model;
using strange.extensions.signal.impl;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class HexPanelView : TypedUiView<IHexTile>
    {
        #region Unity Components
        [SerializeField]
        private Text _xText;
        [SerializeField]
        private Text _yText;
        [SerializeField]
        private Text _terrainText;
        [SerializeField]
        private Text _populationText;
        [SerializeField]
        private Button _raiseArmyButton;
        [SerializeField]
        private Button _countryButton;
        #endregion

        public int X
        {
            set { _xText.text = value.ToString(); }
        }

        public int Y
        {
            set { _yText.text = value.ToString(); }
        }

        public HexTerrainType TerrainType
        {
            set { _terrainText.text = value.ToString(); }
        }

        public int Population
        {
            set
            {
                _populationText.text = value.ToString();
                HasPopulation = true;
            }
        }

        public string CountryName
        {
            set
            {
                _countryButton.GetComponentInChildren<Text>().text = value;
                HasCountry = true;
            }
        }

        public bool HasPopulation
        {
            set { _populationText.transform.parent.gameObject.SetActive(value); }
        }

        public bool CanRaiseArmies
        {
            set { _raiseArmyButton.gameObject.SetActive(value); }
        }

        public bool HasCountry
        {
            set { _countryButton.transform.parent.gameObject.SetActive(value); }
        }

        public Signal RaiseArmySignal = new Signal();
        public Signal ShowCountryPanelSignal = new Signal();

        // Unity listeners.
        public void ButtonRaiseArmy()
        {
            RaiseArmySignal.Dispatch();
        }

        public void ButtonCountry()
        {
            ShowCountryPanelSignal.Dispatch();
        }
    }
}