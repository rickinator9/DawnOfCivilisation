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

        private static IList<Vector3> _vertices = new List<Vector3>();
        private static IList<int> _indices = new List<int>(); 
        private static IList<Color> _colors = new List<Color>();

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
                Triangulate(hexTile);
            }

            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _indices.ToArray();
            _mesh.colors = _colors.ToArray();

            _collider.sharedMesh = _mesh;

            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
        }

        private void Triangulate(IHexTile tile)
        {
            var indexOffset = _vertices.Count;

            for(var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                var corner = HexMetrics.Corners[i];
                var vertex = corner + tile.Center;

                _vertices.Add(vertex);
                _colors.Add(tile.Color);
            }

            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 1); _indices.Add(indexOffset + 2);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 2); _indices.Add(indexOffset + 3);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 3); _indices.Add(indexOffset + 4);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 4); _indices.Add(indexOffset + 5);
        }
    }
}