using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map
{
    public struct HexCoordinates
    {
        public int X { get; set; }

        public int Y
        {
            get { return -X - Z; }
        }

        public int Z { get; set; }

        public HexCoordinates ToOffsetCoordinates()
        {
            return new HexCoordinates
            {
                X = X + Z/2,
                Z = Z,
            };
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates
            {
                X = x - z/2,
                Z = z
            };
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            const float hexWidth = HexMetrics.InnerRadius * 2f;

            var x = position.x / hexWidth;
            var y = -x;

            var offset = position.z / (HexMetrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);
            var iZ = Mathf.RoundToInt(-x - y);

            // Resolve rounding errors
            if (iX + iY + iZ != 0)
            {
                var dX = Mathf.Abs(x - iX);
                var dY = Mathf.Abs(y - iY);
                var dZ = Mathf.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ) iX = -iY - iZ;
                else if (dZ > dY) iZ = -iX - iY;
            }

            return new HexCoordinates
            {
                X = iX,
                Z = iZ
            };
        }

        public override string ToString()
        {
            return string.Format("(X: {0}, Y: {1}, Z: {2})", X, Y, Z);
        }
    }
}