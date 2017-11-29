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
        public Button RaiseArmyButton;
        public Signal<IHexTile> RaiseArmySignal = new Signal<IHexTile>();

        private IHexTile _activeTile;
        private IHexTile Tile
        {
            set
            {
                _activeTile = value;

                var coords = _activeTile.Coordinates.ToOffsetCoordinates();
                XValue.text = coords.X.ToString();
                ZValue.text = coords.Z.ToString();
                TerrainValue.text = _activeTile.TerrainType.ToString();

                if (_activeTile.TerrainType == HexTerrainType.Water)
                {
                    PopulationValue.transform.parent.gameObject.SetActive(false);
                    RaiseArmyButton.gameObject.SetActive(false);
                }
                else
                {
                    var landTile = (ILandTile) _activeTile;

                    PopulationValue.transform.parent.gameObject.SetActive(true);
                    RaiseArmyButton.gameObject.SetActive(landTile.Country != null && landTile.Country.IsPlayerControlled);

                    PopulationValue.text = landTile.Population.ToString();
                }
            }
            get { return _activeTile; }
        }

        public override void UpdateValues(IHexTile obj)
        {
            Tile = obj;
        }

        // Unity listeners.

        public void ButtonRaiseArmy()
        {
            if (Tile.Armies.Length > 0) return;

            Debug.Log("Raising Army...");
            RaiseArmySignal.Dispatch(Tile);
        }
    }
}