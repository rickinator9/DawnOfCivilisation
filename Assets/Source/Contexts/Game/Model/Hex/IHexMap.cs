using System.Linq;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model.Hex
{
    public interface IHexMap
    {
        int Width { get; }
        int Height { get; }

        IHexTile this[int x, int z] { get; set; }

        IHexTile[] AllTiles { get; }
        ILandTile[] LandTiles { get; }
        IWaterTile[] WaterTiles { get; }

        void Initialise(int width, int height);
    }

    public class HexMap : IHexMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private IHexTile[] Tiles { get; set; }
        public IHexTile this[int x, int z]
        {
            get { return Tiles[CalculateIndex(x, z)]; }
            set { Tiles[CalculateIndex(x, z)] = value; }
        }

        public IHexTile[] AllTiles { get { return Tiles; } }

        public ILandTile[] LandTiles
        {
            get { return Tiles.Where(tile => tile.TerrainType != HexTerrainType.Water).Cast<ILandTile>().ToArray(); }
        }

        public IWaterTile[] WaterTiles
        {
            get { return Tiles.Where(tile => tile.TerrainType == HexTerrainType.Water).Cast<IWaterTile>().ToArray(); }
        }

        public void Initialise(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new IHexTile[Width * Height];
        }

        private int CalculateIndex(int x, int z)
        {
            return z*Width + x;
        }
    }
}