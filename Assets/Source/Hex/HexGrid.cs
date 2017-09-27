using UnityEngine;

namespace Assets.Source.Hex
{
    public class HexGrid : MonoBehaviour
    {
        public int Width, Height;
        public HexMesh Mesh;

        public HexTile TilePrefab;

        private HexTile[] _hexTiles;
        private Vector3[] _hexCorners;

        void Start()
        {
            _hexTiles = new HexTile[Width * Height];
            _hexCorners = new Vector3[Width * Height];

            for (var z = 0; z < Height; z++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var i = z*Width + x;
                    var tile = CreateTile(x, z);

                    _hexTiles[i] = tile;
                }
            }
            
            Mesh.Triangulate(_hexTiles);
        }

        private HexTile CreateTile(int x, int z)
        {
            var tile = Instantiate(TilePrefab);
            tile.X = x;
            tile.Z = z;
            tile.Position = new Vector3(GetTileRealXIndex(x, z) * HexMetrics.InnerRadius*2, 0, z * HexMetrics.OuterRadius*1.5f); // TODO: Add Z offset.

            return tile;
        }

        private float GetTileRealXIndex(int x, int z)
        {
            return z%2 == 0 ? x : x + 0.5f;
        }
    }
}