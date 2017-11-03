using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Model;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IMovement
    {
        IHexTile Destination { get; }

        int MovementTime { get; }

        bool HasArrived { get; }

        void Initialise(IHexTile destination, int movementTime);

        void DecrementMovementTime();
    }

    public class Movement : IMovement
    {
        public IHexTile Destination { get; private set; }
        public int MovementTime { get; private set; }

        public bool HasArrived
        {
            get { return MovementTime <= 0; }
        }

        public void Initialise(IHexTile destination, int movementTime)
        {
            Destination = destination;
            MovementTime = movementTime;
        }

        public void DecrementMovementTime()
        {
            MovementTime--;
        }
    }
}