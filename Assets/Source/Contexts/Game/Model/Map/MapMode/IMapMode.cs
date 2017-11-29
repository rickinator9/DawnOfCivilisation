using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Map.MapMode
{
    public interface IMapMode
    {
        Color GetColorForTile(ILandTile tile);

        Color GetStripeColorForTile(ILandTile tile);
    }
}