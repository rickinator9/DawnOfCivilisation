using Assets.Source.Model;
using Assets.Source.Utils;
using UnityEngine;

namespace Assets.Source.Hex
{
    public class HexGrid : MonoBehaviour
    {
        public int Width, Height;
        public HexMesh Mesh;
        public Texture2D Heightmap;

        public int TopBottomRowVertexCount
        {
            get { return Width; }
        }

        public int MiddleRowVertexCount
        {
            get { return Width + 1; }
        }

        public int RowVertexCount
        {
            get { return MiddleRowVertexCount*2 + TopBottomRowVertexCount*2; }
        }

        public int VertexCount
        {
            get
            {
                return TopBottomRowVertexCount + (MiddleRowVertexCount * 2 + TopBottomRowVertexCount)* Height;
            }
        }

        private IHexTile[] _hexTiles;
        private float[] _hexElevations;

        void Start()
        {
            HexMetrics.Width = Width;
            HexMetrics.Height = Height;

            _hexTiles = new IHexTile[Width * Height];

            for (var z = 0; z < Height; z++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var i = z*Width + x;
                    var tile = CreateTile(x, z);

                    _hexTiles[i] = tile;
                }
            }

            _hexElevations = SampleHeights(_hexTiles);

            Mesh.Triangulate(_hexTiles, this);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit))
                {
                    var hitPosition = rayHit.point;
                    var coords = HexCoordinates.FromPosition(hitPosition).ToOffsetCoordinates();

                    var tile = GetTileAtPosition(coords.X, coords.Z);
                    Debug.Log("Hit HexTile " + tile);
                }
            }
        }

        public float[] GetElevationForTile(IHexTile tile)
        {
            var elevations = new float[HexMetrics.Corners.Length];
            var index = GetStartIndexForTile(tile);

            elevations[(int) HexVertexPosition.Top] = _hexElevations[index];
            index += TopBottomRowVertexCount; // Next row;

            elevations[(int) HexVertexPosition.TopLeft] = _hexElevations[index];
            elevations[(int) HexVertexPosition.TopRight] = _hexElevations[index + 1];
            index += MiddleRowVertexCount; // Next row;

            elevations[(int) HexVertexPosition.BottomLeft] = _hexElevations[index];
            elevations[(int) HexVertexPosition.BottomRight] = _hexElevations[index + 1];
            index += MiddleRowVertexCount; // Next row;

            elevations[(int) HexVertexPosition.Bottom] = _hexElevations[index];

            return elevations;
        }

        private IHexTile GetTileAtPosition(int x, int z)
        {
            var index = z*Width + x;
            return _hexTiles[index];
        }

        private IHexTile CreateTile(int x, int z)
        {
            var terrain = EnumUtils.GetRandomValue<HexTerrainType>();
            var tile = new HexTile
            {
                TerrainType = terrain,
                Coordinates = HexCoordinates.FromOffsetCoordinates(x, z)
            };

            Debug.Log(tile);

            return tile;
        }

        private float[] SampleHeights(IHexTile[] tiles)
        {
            var vertices = GetTileVertices(tiles);

            var elevations = new float[vertices.Length];
            for (var e = 0; e < elevations.Length; e++)
            {
                var vertex = vertices[e];
                var elevation = GetElevationAtPosition(vertex);
                elevations[e] = elevation;
            }

            return elevations;
        }

        private Vector3[] GetTileVertices(IHexTile[] tiles)
        {
            var vertices = new Vector3[VertexCount];

            //AddTopRow(tiles, vertices);
            AddLeftColumn(tiles, vertices);
            foreach (var tile in tiles)
            {
                var coords = tile.Coordinates.ToOffsetCoordinates();
                if (!coords.Z.IsEven()) continue;
                AddTileVerticesToArray(tile, vertices);
            }

            return vertices;
        }

        private void AddTopRow(IHexTile[] tiles, Vector3[] vertices)
        {
            var top = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.Top);
            for (var i = 0; i < Width; i++)
            {
                var tile = tiles[i];
                vertices[i] = top + tile.Center;
            }
        }

        private void AddLeftColumn(IHexTile[] tiles, Vector3[] vertices)
        {
            var index = TopBottomRowVertexCount; // Start at the right row.

            var topLeft = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.TopLeft);
            var bottomLeft = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.BottomLeft);
            for (var i = 0; i < Height; i+=2)
            {
                var tile = tiles[i*Width];

                vertices[index] = topLeft + tile.Center;
                index += MiddleRowVertexCount; // Next row.

                vertices[index] = bottomLeft + tile.Center;
                index += MiddleRowVertexCount; // Next row.
                index += TopBottomRowVertexCount; // Next row
                index += TopBottomRowVertexCount; // Next row
            }
        }

        private void AddTileVerticesToArray(IHexTile tile, Vector3[] vertices)
        {
            var index = GetStartIndexForTile(tile);

            vertices[index] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.Top) + tile.Center;
            index += TopBottomRowVertexCount; // Next row. Skip top row since that is already done in AddTopRow.

            //vertices[index] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.TopLeft) + tile.Center;
            vertices[index + 1] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.TopRight) + tile.Center;
            index += MiddleRowVertexCount; // Next row.

            //vertices[index] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.BottomLeft) + tile.Center;
            vertices[index + 1] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.BottomRight) + tile.Center;
            index += MiddleRowVertexCount; // Next row.

            vertices[index] = HexMetrics.GetCornerByVertexPosition(HexVertexPosition.Bottom) + tile.Center;
        }

        private float GetElevationAtPosition(Vector3 position)
        {
            if (Heightmap == null) return 0f;

            var totalCellsWidth = HexMetrics.Width * HexMetrics.InnerRadius * 2f;
            var totalCellsHeight = HexMetrics.Height * HexMetrics.OuterRadius * 1.5f;

            var xPixelsPerUnit = Heightmap.width / totalCellsWidth;
            var zPixelsPerUnit = Heightmap.height / totalCellsHeight;

            var color = Heightmap.GetPixelBilinear(position.x * xPixelsPerUnit, position.z * zPixelsPerUnit);
            var elevation = color.grayscale * HexMetrics.MaxElevation;

            return elevation;
        }

        private int GetStartIndexForTile(IHexTile tile)
        {
            var coords = tile.Coordinates.ToOffsetCoordinates();

            var x = coords.X;
            var z = coords.Z.IsEven() ? coords.Z/2 : coords.Z;

            var offset = !coords.Z.IsEven() ? -2 : 0;
            return RowVertexCount*z + x + offset;
        }
    }
}