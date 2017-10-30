using System;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Hex
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

        public static float GetCost(this HexTerrainType type)
        {
            switch (type)
            {
                case HexTerrainType.Desert:
                    return 2f;
                case HexTerrainType.Plain:
                    return 1f;
                case HexTerrainType.Mountain:
                    return 5f;
                case HexTerrainType.Water:
                    return float.MaxValue;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}