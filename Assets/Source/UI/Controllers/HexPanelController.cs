﻿using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class HexPanelController : MonoBehaviour, IHexPanel
    {
        public Text XValue, ZValue, TerrainValue;

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
            }
            get { return _activeTile; }
        }

        private IArmy _army;

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
            if (_army != null) return;

            Debug.Log("Raising Army...");
            _army = Armies.Instance.CreateArmy();
            _army.Location = Tile;
        }
    }
}