using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using Assets.Source.Utils;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map
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
        private static List<Vector3> _stripeColors = new List<Vector3>(); 
        private static List<Vector3> _terrainTypes = new List<Vector3>(); 

        void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh = new Mesh();
            _mesh.name = "Hex Mesh";

            _collider = gameObject.AddComponent<MeshCollider>();
        }

        public void Triangulate(IHexTile[] tiles, IMapMode mapMode)
        {
            foreach (var hexTile in tiles)
            {
                if (hexTile.TerrainType == HexTerrainType.Water)
                {
                    var waterTile = (IWaterTile) hexTile;
                    TriangulateWaterTile(waterTile);
                }
                else
                {
                    var landTile = (ILandTile) hexTile;
                    TriangulateLandTile(landTile, mapMode);
                }
            }
            
            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_indices, 0);
            _mesh.SetColors(_colors);
            _mesh.SetUVs(1, _stripeColors);
            _mesh.SetUVs(2, _terrainTypes);

            _collider.sharedMesh = _mesh;

            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
            _stripeColors.Clear();
            _terrainTypes.Clear();
        }

        private void TriangulateLandTile(ILandTile tile, IMapMode mapMode)
        {
            var center = tile.Center;

            var color = mapMode.GetColorForTile(tile);
            var stripeColor = mapMode.GetStripeColorForTile(tile);
            for(var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                var terrainType = GetTerrainType(tile, i);

                var corner1 = HexMetrics.Corners[i];
                var corner2 = HexMetrics.Corners[(i + 1)%HexMetrics.Corners.Length];

                AddTriangle(center, center + corner1, center + corner2);
                AddTriangleColor(color);
                AddTriangleStripeColor(stripeColor.ToVector3());
                AddTriangleTerrainType(terrainType);
            }
        }

        private void TriangulateWaterTile(IWaterTile tile)
        {
            const float surfaceFactor = 0.75f;
            var center = tile.Center;
            var depthVector = new Vector3(0, -2, 0);

            var color = tile.Color;
            var stripeColor = Color.black; // No stripes
            for (var i = 0; i < HexMetrics.Corners.Length; i++)
            {
                var terrainType = GetTerrainType(tile, i);

                var corner1 = HexMetrics.Corners[i];
                var corner2 = HexMetrics.Corners[(i + 1)%HexMetrics.Corners.Length];

                var v1 = corner1 * surfaceFactor + center + depthVector;
                var v2 = corner2 * surfaceFactor + center + depthVector;

                AddTriangle(center + depthVector, v1, v2);
                AddTriangleColor(color);
                AddTriangleStripeColor(stripeColor.ToVector3());
                AddTriangleTerrainType(terrainType);

                var v3 = corner1 + center;
                var v4 = corner2 + center;

                AddQuad(v1, v2, v3, v4);
                AddQuadColor(color);
                AddQuadStripeColor(stripeColor.ToVector3());
                AddQuadTerrainType(terrainType);
            }
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

        #region Addition Methods
        #region Triangles
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

        void AddTriangleColor(Color c)
        {
            AddTriangleColor(c, c, c);
        }

        void AddTriangleColor(Color c1, Color c2, Color c3)
        {
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
        }

        void AddTriangleTerrainType(Vector3 t)
        {
            AddTriangleTerrainType(t, t, t);
        }

        void AddTriangleTerrainType(Vector3 t1, Vector3 t2, Vector3 t3)
        {
            _terrainTypes.Add(t1);
            _terrainTypes.Add(t2);
            _terrainTypes.Add(t3);
        }

        void AddTriangleStripeColor(Vector3 s)
        {
            AddTriangleStripeColor(s, s, s);
        }

        void AddTriangleStripeColor(Vector3 s1, Vector3 s2, Vector3 s3)
        {
            _stripeColors.Add(s1);
            _stripeColors.Add(s2);
            _stripeColors.Add(s3);
        }
        #endregion
        #region Quads
        void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            var vertexIndex = _vertices.Count;

            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _vertices.Add(v4);

            _indices.Add(vertexIndex);
            _indices.Add(vertexIndex + 2);
            _indices.Add(vertexIndex + 1);
            _indices.Add(vertexIndex + 1);
            _indices.Add(vertexIndex + 2);
            _indices.Add(vertexIndex + 3);
        }

        void AddQuadColor(Color c)
        {
            AddQuadColor(c, c, c, c);
        }

        void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
        {
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
            _colors.Add(c4);
        }

        void AddQuadTerrainType(Vector3 t)
        {
            AddQuadTerrainType(t, t, t, t);
        }

        void AddQuadTerrainType(Vector3 t1, Vector3 t2, Vector3 t3, Vector3 t4)
        {
            _terrainTypes.Add(t1);
            _terrainTypes.Add(t2);
            _terrainTypes.Add(t3);
            _terrainTypes.Add(t4);
        }

        void AddQuadStripeColor(Vector3 s)
        {
            AddQuadStripeColor(s, s, s, s);
        }

        void AddQuadStripeColor(Vector3 s1, Vector3 s2, Vector3 s3, Vector3 s4)
        {
            _stripeColors.Add(s1);
            _stripeColors.Add(s2);
            _stripeColors.Add(s3);
            _stripeColors.Add(s4);
        }
        #endregion
        #endregion
    }
}