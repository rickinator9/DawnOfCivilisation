using System;
using UnityEngine;

namespace Assets.Source.Model
{
    public enum HexTerrainType
    {
         Desert,
         Plain,
         Mountain,

         Water
    }

    public static class HexTerrainTypeExtensions
    {
        public static Color GetColor(this HexTerrainType type)
        {
            switch (type)
            {
                case HexTerrainType.Desert:
                    return new Color(1f, 1f, 0f); // Yellow
                case HexTerrainType.Plain:
                    return Color.green;
                case HexTerrainType.Mountain:
                    return new Color(139f / 255f, 69f / 255f, 19f / 255f); // Brown
                case HexTerrainType.Water:
                    return Color.blue;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}