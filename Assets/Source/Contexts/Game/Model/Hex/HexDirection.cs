namespace Assets.Source.Contexts.Game.Model.Hex
{
    public enum HexDirection
    {
        NorthWest,
        NorthEast,
        East,
        SouthEast,
        SouthWest,
        West
    }

    public static class HexDirectionExtensions
    {
        public static HexDirection Opposite(this HexDirection direction)
        {
            var index = (int) direction;
            index = (index + 3)%6;
            return (HexDirection) index;
        }
    }
}