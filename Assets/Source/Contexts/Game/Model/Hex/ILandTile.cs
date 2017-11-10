using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Hex
{
    public interface ILandTile : IHexTile
    {
        int Population { get; }

        ICountry Country { get; set; }

        void Initialise(HexCoordinates coordinates, HexTerrainType terrainType, int population);
    }

    public class LandTile : HexTile, ILandTile
    {
        public override Color Color
        {
            get
            {
                if (Country == null) return TerrainType.GetColor();

                return Country.Color;
            }
        }

        private HexTerrainType _terrainType;
        public override HexTerrainType TerrainType { get { return _terrainType; } }

        public int Population { get; private set; }

        private ICountry _country;

        public ICountry Country
        {
            get { return _country; }
            set
            {
                _country = value;
                foreach (var signal in RefreshHexGridViewSignals)
                {
                    signal.Dispatch();
                }
            }
        }

        public void Initialise(HexCoordinates coordinates, HexTerrainType terrainType, int population)
        {
            Coordinates = coordinates;
            _terrainType = terrainType;
            Population = population;
        }
    }
}