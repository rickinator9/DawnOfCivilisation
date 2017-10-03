using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Hex
{
    public enum HexSide
    {
         Top,
         Bottom,

         Left,
         Right
    }

    public static class HexSideExtensions
    {
        public static HexSide GetSideByCornerIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return HexSide.Top;
                case 1:
                case 2:
                    return HexSide.Right;
                case 3:
                    return HexSide.Bottom;
                case 4:
                case 5:
                    return HexSide.Left;
                default:
                    throw new ArgumentOutOfRangeException("index", index, null);
            }
        }

        public static IList<HexSide> GetSidesForCorner(int cornerIndex)
        {
            var sides = new List<HexSide>();
            switch (cornerIndex)
            {
                case 0: // Top
                    sides.Add(HexSide.Top);
                    sides.Add(HexSide.Left);
                    sides.Add(HexSide.Right);
                    break;
                case 1: // Top - Right
                    sides.Add(HexSide.Top);
                    sides.Add(HexSide.Right);
                    break;
                case 2: // Bottom - Right
                    sides.Add(HexSide.Bottom);
                    sides.Add(HexSide.Right);
                    break;
                case 3: // Bottom
                    sides.Add(HexSide.Bottom);
                    sides.Add(HexSide.Left);
                    sides.Add(HexSide.Right);
                    break;
                case 4: // Bottom - Left
                    sides.Add(HexSide.Bottom);
                    sides.Add(HexSide.Left);
                    break;
                case 5: // Top - Left
                    sides.Add(HexSide.Top);
                    sides.Add(HexSide.Left);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("cornerIndex", cornerIndex, null);
            }

            return sides;
        }

        public static Vector3 GetSidePosition(this HexSide side)
        {
            var corners = HexMetrics.Corners;
            switch (side)
            {
                case HexSide.Top:
                    return corners[0];
                case HexSide.Bottom:
                    return corners[3];
                case HexSide.Left:
                    return (corners[4] + corners[5])*0.5f;
                case HexSide.Right:
                    return (corners[1] + corners[2]) * 0.5f;
                default:
                    throw new ArgumentOutOfRangeException("side", side, null);
            }
        }
    }
}