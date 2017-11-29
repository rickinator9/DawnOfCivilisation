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
                if(hexTile.TerrainType == HexTerrainType.Water) continue;

                var landTile = (ILandTile) hexTile;
                Triangulate(landTile, mapMode);
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

        private void Triangulate(ILandTile tile, IMapMode mapMode)
        {
            var indexOffset = _vertices.Count;

            //_vertices.Add(tile.Center);
            //_colors.Add(tile.Color);
            //_stripeColors.Add(GetStripeColor(tile).ToVector3());
            //_terrainTypes.Add(GetTerrainType(tile, -1));
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

                //AddVertex(tile, mapMode, i, indexOffset);
            }
        }

        private void AddVertex(ILandTile tile, IMapMode mapMode, int cornerIndex, int indexOffset)
        {
            var corner = HexMetrics.Corners[cornerIndex];
            var vertex = corner + tile.Center;

            _vertices.Add(vertex);
            _colors.Add(mapMode.GetColorForTile(tile));

            _stripeColors.Add(mapMode.GetStripeColorForTile(tile).ToVector3());
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

        private Color GetStripeColor(ILandTile tile)
        {
            if (tile.Country != tile.Controller)
            {
                return tile.Controller.Color;
            }

            return new Color(0,0,0,0);
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
        #endregion
    }
}