using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Hex
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
        private static IDictionary<HexSide, float> _elevations = new Dictionary<HexSide, float>(); 

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
                Triangulate(hexTile /*, grid.GetElevationForTile(hexTile)*/);
            }

            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _indices.ToArray();
            _mesh.colors = _colors.ToArray();

            _collider.sharedMesh = _mesh;

            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
        }

        private void Triangulate(IHexTile tile /*, float[] elevations*/)
        {
            _elevations.Clear();
            SampleElevations(tile.Center);

            var indexOffset = _vertices.Count;

            for(var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                var position = (HexVertexPosition) i;
                var corner = HexMetrics.Corners[i];
                var vertex = corner + tile.Center;
                //vertex.y = elevations[i];

                _vertices.Add(vertex);
                _colors.Add(tile.Color);
            }

            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 1); _indices.Add(indexOffset + 2);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 2); _indices.Add(indexOffset + 3);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 3); _indices.Add(indexOffset + 4);
            _indices.Add(indexOffset + 0); _indices.Add(indexOffset + 4); _indices.Add(indexOffset + 5);
        }

        private void SampleElevations(Vector3 center)
        {
            var sides = Enum.GetValues(typeof (HexSide));
            foreach (HexSide side in sides)
            {
                var position = side.GetSidePosition();
                var elevation = GetElevationAtPosition(position + center);
                _elevations[side] = elevation;
            }
        }

        private float GetElevationForCorner(int cornerIndex)
        {
            var sides = HexSideExtensions.GetSidesForCorner(cornerIndex);
            return 0f;
            return GetAverageElevation(sides);
        }

        private float GetAverageElevation(IList<HexSide> sides)
        {
            var sum = sides.Sum(side => _elevations[side]);
            return sum/sides.Count;
        }

        private float GetElevationAtPosition(Vector3 position)
        {
            if (Heightmap == null) return 0f;

            var totalCellsWidth = HexMetrics.Width*HexMetrics.InnerRadius*2f;
            var totalCellsHeight = HexMetrics.Height*HexMetrics.OuterRadius*1.5f;

            var xPixelsPerUnit = Heightmap.width/totalCellsWidth;
            var zPixelsPerUnit = Heightmap.height/totalCellsHeight;

            var color = Heightmap.GetPixelBilinear(position.x*xPixelsPerUnit, position.z*zPixelsPerUnit);
            var elevation = color.grayscale * HexMetrics.MaxElevation;

            return elevation;
        }
    }
}