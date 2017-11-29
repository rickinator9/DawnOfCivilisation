using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map
{
    public interface IWaterTile : IHexTile
    {
        void Initialise(HexCoordinates coordinates);
    }

    public class WaterTile : HexTile, IWaterTile
    {
        public override Color Color
        {
            get { return Color.black; }
        }

        public override HexTerrainType TerrainType
        {
            get { return HexTerrainType.Water; }
        }

        public void Initialise(HexCoordinates coordinates)
        {
            Coordinates = coordinates;
        }
    }
}