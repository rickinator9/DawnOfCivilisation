using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map
{
    public interface ILandTile : IHexTile
    {
        int Population { get; }

        ICountry Country { get; set; }

        ICountry Controller { get; set; }

        ICity City { get; set; }

        bool HasCity { get; }

        bool AllowsCity { get; }

        void Initialise(HexCoordinates coordinates, HexTerrainType terrainType, int population);
    }

    public class LandTile : HexTile, ILandTile
    {
        public override Color Color
        {
            get
            {
                if (Country == null) return new Color(0, 0, 0, 0);

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

        private ICountry _controller;

        public ICountry Controller
        {
            get { return _controller; }
            set
            {
                if (_country == null) return;

                _controller = value;
                foreach (var signal in RefreshHexGridViewSignals)
                {
                    signal.Dispatch();
                }
            }
        }

        public ICity City { get; set; }

        public bool HasCity { get { return City != null; } }

        public bool AllowsCity
        {
            get
            {
                var landNeighboursSet = new HashSet<ILandTile>();
                foreach (var landNeighbor in GetLandNeighbors(this))
                {
                    landNeighboursSet.Add(landNeighbor);
                    foreach (var neighbor in GetLandNeighbors(landNeighbor))
                    {
                        landNeighboursSet.Add(neighbor);
                    }
                }

                return landNeighboursSet.All(hex => !hex.HasCity);
            }
        }

        public void Initialise(HexCoordinates coordinates, HexTerrainType terrainType, int population)
        {
            Coordinates = coordinates;
            _terrainType = terrainType;
            Population = population;
        }

        private IList<ILandTile> GetLandNeighbors(IHexTile tile)
        {
            return tile.Neighbors.Where(neighbor => neighbor != null && neighbor.TerrainType != HexTerrainType.Water).Cast<ILandTile>().ToList();
        } 
    }
}