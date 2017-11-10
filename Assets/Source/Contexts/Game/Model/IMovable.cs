using Assets.Source.Contexts.Game.Model.Hex;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IMovable
    {
        IMovementPath MovementPath { get; set; }
        
        bool IsMoving { get; set; }

        IHexTile Location { get; set; }

        void OnArrivalInTile(IHexTile tile);
    }
}