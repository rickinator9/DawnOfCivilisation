using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map.MapMode
{
    public class PoliticalMapMode : IMapMode
    {
        public Color GetColorForTile(ILandTile tile)
        {
            return tile.Color;
        }

        public Color GetStripeColorForTile(ILandTile tile)
        {
            if (tile.Country != tile.Controller)
            {
                return tile.Controller.Color;
            }

            return new Color(0, 0, 0, 0);
        }
    }
}