using System.Collections.Generic;
using System.Linq;
using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Hex
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        public Texture2D Heightmap;

        private Mesh _mesh;
        private MeshCollider _collider;

        private static List<Vector3> _vertices = new List<Vector3>();
        private static List<int> _indices = new List<int>(); 
        private static List<Color> _colors = new List<Color>();
        private static List<Vector3> _terrainTypes = new List<Vector3>(); 

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            _mesh.name = "Hex Mesh";

            _collider = gameObject.AddComponent<MeshCollider>();
        }

        public void Triangulate(IHexTile[] tiles)
        {
            foreach (var hexTile in tiles)
            {
                //if(hexTile.TerrainType == HexTerrainType.Water) continue;

                Triangulate(hexTile);
            }

            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_indices, 0);
            _mesh.SetColors(_colors);
            _mesh.SetUVs(2, _terrainTypes);

            _collider.sharedMesh = _mesh;

            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
            _terrainTypes.Clear();
        }

        private void Triangulate(IHexTile tile)
        {
            var indexOffset = _vertices.Count;

            _vertices.Add(tile.Center);
            _colors.Add(tile.Color);
            _terrainTypes.Add(GetTerrainType(tile, -1));
            for(var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                AddVertex(tile, i, indexOffset);
            }

            //_indices.Add(indexOffset + 0); _indices.Add(indexOffset + 1); _indices.Add(indexOffset + 2);
            //_indices.Add(indexOffset + 0); _indices.Add(indexOffset + 2); _indices.Add(indexOffset + 3);
            //_indices.Add(indexOffset + 0); _indices.Add(indexOffset + 3); _indices.Add(indexOffset + 4);
            //_indices.Add(indexOffset + 0); _indices.Add(indexOffset + 4); _indices.Add(indexOffset + 5);
        }

        private void AddVertex(IHexTile tile, int cornerIndex, int indexOffset)
        {
            var corner = HexMetrics.Corners[cornerIndex];
            var vertex = corner + tile.Center;

            _vertices.Add(vertex);
            _colors.Add(tile.Color);

            _terrainTypes.Add(GetTerrainType(tile, cornerIndex));

            _indices.Add(indexOffset);
            _indices.Add(indexOffset + cornerIndex + 1);
            if (cornerIndex == 5)
            {
                _indices.Add(indexOffset + 1);
            }
            else _indices.Add(indexOffset + cornerIndex + 2);
        }

        private Vector3 GetTerrainType(IHexTile tile, int cornerIndex)
        {
            //if (cornerIndex == -1)
            //{
                return new Vector3(tile.TerrainType.GetTerrainIndex(),
                                   tile.TerrainType.GetTerrainIndex(),
                                   tile.TerrainType.GetTerrainIndex());
            //}

            var terrainType = Vector3.zero;
            terrainType.x = tile.TerrainType.GetTerrainIndex();

            if (tile.Neighbors[cornerIndex] != null)
            {
                var neighbor = tile.Neighbors[cornerIndex];
                terrainType.y = neighbor.TerrainType.GetTerrainIndex();
            }
            else terrainType.y = tile.TerrainType.GetTerrainIndex();

            var nextCornerIndex = (cornerIndex + 1)%HexMetrics.Corners.Length;

            if (tile.Neighbors[nextCornerIndex] != null)
            {
                var neighbor = tile.Neighbors[nextCornerIndex];
                terrainType.z = neighbor.TerrainType.GetTerrainIndex();
            }
            else terrainType.z = tile.TerrainType.GetTerrainIndex();

            return terrainType;
        }
    }
}