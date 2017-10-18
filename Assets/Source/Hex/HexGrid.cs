using Assets.Source.Model;
using Assets.Source.Model.Impl;
using Assets.Source.UI;
using Assets.Source.UI.Controllers;
using Assets.Source.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Source.Hex
{
    public class HexGrid : MonoBehaviour, IHexGridView
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
        private bool NeedsRefresh { get; set; }

        private IHexTile[] _hexTiles;
        private float[] _hexElevations;
        private IPanels _panels;

        void Start()
        {
            var players = Players.Instance;
            var player = new LocalPlayer {Country = new Country("Sumeria")};
            players.LocalPlayer = player;

            HexMetrics.Width = Width;
            HexMetrics.Height = Height;

            _hexTiles = new IHexTile[Width * Height];
            _panels = PanelsController.Instance;

            for (var z = 0; z < Height; z++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var i = z*Width + x;
                    var tile = CreateTile(x, z);

                    _hexTiles[i] = tile;

                    if (x > 0)
                    {
                        var leftTile = _hexTiles[i - 1];
                        AddAdjacency(leftTile, tile, HexDirection.East);
                    }
                    if (z > 0)
                    {
                        if (z.IsEven())
                        {
                            var northWestTile = _hexTiles[i - Width];
                            AddAdjacency(northWestTile, tile, HexDirection.SouthEast);
                            if (x > 0)
                            {
                                var northEastTile = _hexTiles[i - Width - 1];
                                AddAdjacency(northEastTile, tile, HexDirection.SouthWest);
                            }
                        }
                        else
                        {
                            var northEastTile = _hexTiles[i - Width];
                            AddAdjacency(northEastTile, tile, HexDirection.SouthWest);
                            if (x < Width - 1)
                            {
                                var northWestTile = _hexTiles[i - Width + 1];
                                AddAdjacency(northWestTile, tile, HexDirection.SouthEast);
                            }
                        }
                    }
                }
            }

            //_hexElevations = SampleHeights(_hexTiles);

            Mesh.Triangulate(_hexTiles, this);
        }

        void Update()
        {
            if(NeedsRefresh) OnRefresh();

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                IHexTile tile;
                RaycastHit hit;

                var foundTile = FindTileWithRayCast(out tile, out hit);
                if (foundTile)
                {
                    if (hit.transform.gameObject.tag.Equals("HexGrid"))
                    {
                        _panels.HideAll();
                        _panels.HexPanel.ShowForHexTile(tile);
                    }
                    else if (hit.transform.gameObject.tag.Equals("Army"))
                    {
                        var army = tile.Armies[0];
                        Players.Instance.LocalPlayer.SelectedObject = army;

                        _panels.HideAll();
                        _panels.ArmyPanel.ShowForArmy(army);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                IHexTile tile;

                var foundTile = FindTileWithRayCast(out tile, out hit);
                if (foundTile && tile.TerrainType != HexTerrainType.Water)
                {
                    var selectedObject = Players.Instance.LocalPlayer.SelectedObject;
                    if(selectedObject != null) selectedObject.OnRightClickOnTile(tile);
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
            tile.HexGridView = this;

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

        private bool FindTileWithRayCast(out IHexTile tile, out RaycastHit hit)
        {
            tile = null;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var hitPosition = hit.point;
                var coords = HexCoordinates.FromPosition(hitPosition).ToOffsetCoordinates();

                tile = GetTileAtPosition(coords.X, coords.Z);
                return true;
            }
            return false;
        }

        private void AddAdjacency(IHexTile tile1, IHexTile tile2, HexDirection direction)
        {
            tile1.AddNeighbor(tile2, direction);
            tile2.AddNeighbor(tile1, direction.Opposite());
        }

        public void Refresh()
        {
            NeedsRefresh = true;
        }


        private void OnRefresh()
        {
            Mesh.Triangulate(_hexTiles, this);

            NeedsRefresh = false;
        }
    }
}