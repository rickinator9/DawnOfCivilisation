using Assets.Source.Hex;
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

        void AddNeighbor(IHexTile tile, HexDirection direction);

        bool HasNeighbor(IHexTile tile);
    }
}