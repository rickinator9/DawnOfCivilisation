using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Contexts.Game.Model.Pathfinding;
using Assets.Source.Core.IoC;
using Assets.Source.Model;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands.Army
{
    public struct MovementPathParams
    {
        public IHexTile Start;

        public IHexTile Destination;
    }

    #region Signals
    /// <summary>
    /// IArmy: The army the path will be created for.
    /// ArmyMovementPathParams: The start and destination hexes.
    /// </summary>
    public class CreateMovementPathSignal : Signal<IArmy, MovementPathParams>
    {
        
    }
    #endregion

    public class CreateMovementPathCommand : Command
    {
#region From Signal
        [Inject]
        public IArmy Army { get; set; }

        [Inject]
        public MovementPathParams MovementPathParams { get; set; }
#endregion

#region Dependencies
        [Inject]
        public IPathfinding Pathfinding { get; set; }
#endregion

        public IHexTile Start { get { return MovementPathParams.Start; } }
        public IHexTile Destination { get { return MovementPathParams.Destination; } }

        public override void Execute()
        {
            if (Start != Destination)
            {
                var grid = Pathfinding;
                var tilePath = grid.FindPath(Start, Destination);

                var movements = new List<IMovement>();
                for (var i = 1; i < tilePath.Count; i++)
                {
                    var previousTile = tilePath[i - 1];
                    var destination = tilePath[i];

                    var moveCost = (previousTile.TerrainType.GetCost() + destination.TerrainType.GetCost()) / 2;
                    var moveTime = (int)Mathf.Round(moveCost);

                    var movement = injectionBinder.GetInstance<IMovement>(CustomContextKeys.NewInstance);
                    movement.Initialise(destination, moveTime);
                    movements.Add(movement);
                }

                var movementPath = injectionBinder.GetInstance<IMovementPath>(CustomContextKeys.NewInstance);
                movementPath.Initialise(movements, Army);
                Army.MovementPath = movementPath;
                Army.IsMoving = true;
            }
        }
    }
}