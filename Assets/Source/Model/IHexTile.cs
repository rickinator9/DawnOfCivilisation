using UnityEngine;

namespace Assets.Source.Model
{
    public interface IHexTile
    {
        int X { get; set; }

        int Z { get; set; }

        Vector3 Center { get; }

        Color Color { get; }

        HexTerrainType TerrainType { get; set; }
    }
}