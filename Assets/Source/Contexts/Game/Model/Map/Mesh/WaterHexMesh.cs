using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map.Mesh
{
    public class WaterHexMesh : BaseHexMesh
    {
        private static List<Vector3> _vertices = new List<Vector3>();
        private static List<int> _indices = new List<int>();

        protected override void Initialise()
        {
            Mesh.name = "Water Hex Mesh";
        }

        protected override void TriangulateTiles(IHexTile[] tiles, IMapMode mapMode)
        {
            foreach (var hexTile in tiles)
            {
                if (hexTile.TerrainType == HexTerrainType.Water)
                {
                    var waterTile = (IWaterTile) hexTile;
                    TriangulateWaterTile(waterTile);
                }
            }
        }

        private void TriangulateWaterTile(IWaterTile waterTile)
        {
            var center = waterTile.Center + new Vector3(0, -1f, 0);

            for (var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                var corner1 = HexMetrics.Corners[i];
                var corner2 = HexMetrics.Corners[(i + 1) % HexMetrics.Corners.Length];

                AddTriangle(center, center + corner1, center + corner2);
            }
        }

        protected override void SetMeshProperties()
        {
            Mesh.SetVertices(_vertices);
            Mesh.SetTriangles(_indices, 0);
        }

        protected override void ClearMeshProperties()
        {
            _vertices.Clear();
            _indices.Clear();
        }

        void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var vertexIndex = _vertices.Count;

            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);

            _indices.Add(vertexIndex);
            _indices.Add(vertexIndex + 1);
            _indices.Add(vertexIndex + 2);
        }
    }
}