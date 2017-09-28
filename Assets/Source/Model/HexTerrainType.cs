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
                    return Color.white;
                case HexTerrainType.Plain:
                    return Color.green;
                case HexTerrainType.Mountain:
                    return Color.grey;
                case HexTerrainType.Water:
                    return Color.blue;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}