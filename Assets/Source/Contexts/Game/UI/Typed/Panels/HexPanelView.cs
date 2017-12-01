using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Model;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class HexPanelView : TypedUiView<IHexTile>
    {
        public Text XValue, ZValue, TerrainValue, PopulationValue;
        public Button RaiseArmyButton, CountryButton;
        public Signal<IHexTile> RaiseArmySignal = new Signal<IHexTile>();
        public Signal<ICountry> ShowCountryPanelSignal = new Signal<ICountry>();

        private IHexTile _activeTile;

        public override void UpdateValues(IHexTile obj)
        {
            _activeTile = obj;

            var coords = _activeTile.Coordinates.ToOffsetCoordinates();
            XValue.text = coords.X.ToString();
            ZValue.text = coords.Z.ToString();
            TerrainValue.text = _activeTile.TerrainType.ToString();

            if (_activeTile.TerrainType == HexTerrainType.Water)
            {
                PopulationValue.transform.parent.gameObject.SetActive(false);
                RaiseArmyButton.gameObject.SetActive(false);
                CountryButton.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                var landTile = (ILandTile)_activeTile;

                PopulationValue.transform.parent.gameObject.SetActive(true);
                RaiseArmyButton.gameObject.SetActive(landTile.Country != null && landTile.Country.IsPlayerControlled);
                CountryButton.transform.parent.gameObject.SetActive(landTile.Country != null);
                if (landTile.Country != null) CountryButton.GetComponentInChildren<Text>().text = landTile.Country.Name;

                PopulationValue.text = landTile.Population.ToString();
            }
        }

        // Unity listeners.
        public void ButtonRaiseArmy()
        {
            if (_activeTile.Armies.Length > 0) return;

            RaiseArmySignal.Dispatch(_activeTile);
        }

        public void ButtonCountry()
        {
            ShowCountryPanelSignal.Dispatch(((ILandTile)_activeTile).Country);
        }
    }
}