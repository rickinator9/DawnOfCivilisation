using System.Collections.Generic;
using System.Linq;
using Assets.Source.Model;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Hex
{
    public interface IHexTile
    {
        HexCoordinates Coordinates { get; set; }

        Vector3 Center { get; }

        Color Color { get; }

        HexTerrainType TerrainType { get; set; }

        IHexTile[] Neighbors { get; }

        ICountry Country { get; set; }

        IArmy[] Armies { get; }

        IList<Signal> RefreshSignals { get; } 

        void AddNeighbor(IHexTile tile, HexDirection direction);

        bool HasNeighbor(IHexTile tile);

        void AddArmy(IArmy army);

        void RemoveArmy(IArmy army);
    }

    public class HexTile : IHexTile
    {
        public HexCoordinates Coordinates { get; set; }

        private bool _centerInitialised = false;
        private Vector3 _center;
        public Vector3 Center
        {
            get
            {
                if (!_centerInitialised)
                {
                    var coords = Coordinates.ToOffsetCoordinates();
                    var x = coords.X;
                    var z = coords.Z;

                    var xPos = (x + z * 0.5f - z / 2) * HexMetrics.InnerRadius * 2; // z*0.5f - z/2 either results in 0 or 0.5 depending on the rounding of z/2.
                    var zPos = z * HexMetrics.OuterRadius * 1.5f; // Only multiply by 1.5f since hexes hook into eachother.

                    _center = new Vector3(xPos, 0, zPos);
                }

                return _center;
            }
        }

        public Color Color
        {
            get
            {
                if (Country == null) return TerrainType.GetColor();

                return Country.Color;
            }
        }

        public HexTerrainType TerrainType { get; set; }

        private IHexTile[] _neighbors = new IHexTile[6];
        public IHexTile[] Neighbors
        {
            get { return _neighbors.ToArray(); }
        }

        private ICountry _country;

        public ICountry Country
        {
            get { return _country; }
            set
            {
                _country = value;
                foreach (var refreshSignal in RefreshSignals)
                {
                    refreshSignal.Dispatch();
                }
            }
        }

        private IList<IArmy> _armies = new List<IArmy>();

        public IArmy[] Armies
        {
            get { return _armies.ToArray(); }
        }

        private IList<Signal> _refreshSignals = new List<Signal>();
        public IList<Signal> RefreshSignals { get { return _refreshSignals; } }

        public void AddNeighbor(IHexTile tile, HexDirection direction)
        {
            var i = (int)direction;
            _neighbors[i] = tile;
        }

        public bool HasNeighbor(IHexTile tile)
        {
            return Neighbors.Any(neighbor => tile == neighbor);
        }

        public void AddArmy(IArmy army)
        {
            _armies.Add(army);
        }

        public void RemoveArmy(IArmy army)
        {
            _armies.Remove(army);
        }

        public override string ToString()
        {
            var coords = Coordinates.ToOffsetCoordinates();

            return string.Format("HexTile{0}: terrain: {1}.", coords, TerrainType);
        }
    }
}