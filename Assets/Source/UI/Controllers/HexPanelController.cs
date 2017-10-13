﻿using Assets.Source.Model;
using Assets.Source.Model.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class HexPanelController : MonoBehaviour, IHexPanel
    {
        public Text XValue, ZValue, TerrainValue;
        public Button RaiseArmyButton;

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

                RaiseArmyButton.gameObject.SetActive(_activeTile.TerrainType != HexTerrainType.Water);
            }
            get { return _activeTile; }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowForHexTile(IHexTile tile)
        {
            Tile = tile;

            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // Unity listeners.

        public void ButtonRaiseArmy()
        {
            if (Tile.Armies.Length > 0) return;

            Debug.Log("Raising Army...");
            var army = Armies.Instance.CreateArmy();
            army.Location = Tile;
            army.Country = Players.Instance.LocalPlayer.Country;

            Tile.Country = army.Country;
        }
    }
}