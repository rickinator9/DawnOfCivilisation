using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Map;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Commands.ProcessCommands
{
#region Signals
    public class ProcessMovablesSignal : Signal { }
#endregion

    public class ProcessMovablesCommand : Command
    {
        [Inject]
        public IMovables Movables { get; set; }

        public override void Execute()
        {
            foreach (var movable in Movables.AllMovables)
            {
                if (movable.IsMoving)
                {
                    var path = movable.MovementPath;
                    var next = path.NextMovement;
                    next.DecrementMovementTime();
                    if (next.HasArrived)
                    {
                        OnArrival(movable, next.Destination);
                        path.SetMovementComplete();
                        if (path.IsComplete)
                        {
                            movable.IsMoving = false;
                        }
                    }
                }
            }
        }

        private void OnArrival(IMovable movable, IHexTile tile)
        {
            movable.Location = tile;
            movable.OnArrivalInTile(tile);
        }
    }
}