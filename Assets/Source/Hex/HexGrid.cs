using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Hex
{
    public class HexGrid : MonoBehaviour
    {
        public int Width, Height;
        public HexMesh Mesh;

        private IHexTile[] _hexTiles;
        private Vector3[] _hexCorners;

        void Start()
        {
            _hexTiles = new IHexTile[Width * Height];
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

        private IHexTile CreateTile(int x, int z)
        {
            var tile = new HexTile
            {
                TerrainType = HexTerrainType.Desert,
                X = x,
                Z = z
            };

            return tile;
        }
    }
}