using Assets.Source.Hex;
using UnityEngine;

namespace Assets.Source.Model
{
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
            get { return TerrainType.GetColor(); }
        }

        public HexTerrainType TerrainType { get; set; }

        public override string ToString()
        {
            var coords = Coordinates.ToOffsetCoordinates();

            return string.Format("HexTile{0}: terrain: {1}.", coords, TerrainType);
        }
    }
}