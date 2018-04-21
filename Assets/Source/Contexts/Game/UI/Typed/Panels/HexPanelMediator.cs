using Assets.Source.Contexts.Game.Commands.Army;
using Assets.Source.Contexts.Game.Commands.UI;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Model;
using System;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{

    public class HexPanelMediator : TypedUiMediator<HexPanelView, HexPanelView, IHexTile>
    {
        private IHexTile _tile;

        [Inject]
        public CreatePlayerArmySignal CreatePlayerArmyDispatcher { get; set; }

        [Inject]
        public ShowUiPanelExclusivelySignal ShowUiPanelExclusivelyDispatcher { get; set; }

        protected override UiType UiType
        {
            get { return UiType.HexPanel; }
        }

        public override void OnRegister()
        {
            base.OnRegister();

            View.RaiseArmySignal.AddListener(RaiseArmy);
            View.ShowCountryPanelSignal.AddListener(ShowCountryPanel);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            View.RaiseArmySignal.RemoveListener(RaiseArmy);
            View.ShowCountryPanelSignal.RemoveListener(ShowCountryPanel);
        }

        protected override void ShowUiPanelForObject(IHexTile obj)
        {
            _tile = obj;
            if (_tile == null) return;

            switch(_tile.TerrainType) {
                case HexTerrainType.Water:
                    ShowUiPanelForWaterTile(_tile as IWaterTile);
                    break;
                case HexTerrainType.Plain:
                case HexTerrainType.Mountain:
                case HexTerrainType.Desert:
                    ShowUiPanelForLandTile(_tile as ILandTile);
                    break;
                default:
                    throw new ArgumentException($"There is no case handling {_tile.TerrainType.ToString()}!");
            }
        }

        private void ShowUiPanelForWaterTile(IWaterTile waterTile)
        {
            // Set the coordinates in the view.
            var coords = _tile.Coordinates;
            View.X = coords.X;
            View.Y = coords.Y;

            // Set the terrain in the view.
            var terrain = _tile.TerrainType;
            View.TerrainType = terrain;

            // Disable some stuff that should not show up for a water tile.
            View.HasCountry = false;
            View.CanRaiseArmies = false;
            View.HasPopulation = false;

            // When we are done, show the view.
            View.Show();
        }

        private void ShowUiPanelForLandTile(ILandTile landTile)
        {
            // Set the coordinates in the view.
            var coords = _tile.Coordinates;
            View.X = coords.X;
            View.Y = coords.Y;

            // Set the terrain in the view.
            var terrain = _tile.TerrainType;
            View.TerrainType = terrain;

            // Set the political aspects of the view.
            if (landTile.Country != null) {
                View.CountryName = landTile.Country.Name;
                View.CanRaiseArmies = landTile.Country.IsHumanControlled;
            } else {
                View.HasCountry = false;
                View.CanRaiseArmies = false;
            }

            // Set the population of the view.
            View.Population = landTile.Population;

            // When we are done, show the view.
            View.Show();
        }

        private void RaiseArmy()
        {
            if (_tile == null) return;

            CreatePlayerArmyDispatcher.Dispatch(_tile);
        }

        private void ShowCountryPanel()
        {
            if (_tile == null) return;
            if (_tile.TerrainType == HexTerrainType.Water) return;

            var landTile = (ILandTile)_tile;

            if (landTile.Country == null) return;

            var country = landTile.Country;
            ShowUiPanelExclusivelyDispatcher.Dispatch(UiType.CountryPanel, country);
        }
    }
}