using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Hex;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Model
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
}