using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Contexts.Game.Model.Pathfinding;
using Assets.Source.Model;
using Assets.Source.Model.Background.Impl;
using Assets.Source.Model.Impl;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands.Army
{
    public struct ArmyMovementPathParams
    {
        public IHexTile Start;

        public IHexTile Destination;
    }

    #region Signals
    /// <summary>
    /// IArmy: The army the path will be created for.
    /// ArmyMovementPathParams: The start and destination hexes.
    /// </summary>
    public class CreateArmyMovementPathSignal : Signal<IArmy, ArmyMovementPathParams>
    {
        
    }
    #endregion

    public class CreateArmyMovementPathCommand : Command
    {
        [Inject]
        public IArmy Army { get; set; }

        [Inject]
        public ArmyMovementPathParams ArmyMovementPathParams { get; set; }

        [Inject]
        public IPathfinding Pathfinding { get; set; }

        public IHexTile Start { get { return ArmyMovementPathParams.Start; } }
        public IHexTile Destination { get { return ArmyMovementPathParams.Destination; } }

        public override void Execute()
        {
            if (Start != Destination)
            {
                var grid = Pathfinding;
                var tilePath = grid.FindPath(Start, Destination);

                var dateManager = DateManager.Instance;
                var timeManager = TimeManager.Instance;
                var backgroundTaskManager = BackgroundTaskManager.Instance;

                var arrivalDate = timeManager.CurrentDate;
                for (var i = 1; i < tilePath.Count; i++)
                {
                    var previousTile = tilePath[i - 1];
                    var destination = tilePath[i];

                    var moveCost = (previousTile.TerrainType.GetCost() + destination.TerrainType.GetCost()) / 2;
                    var moveTime = (int)Mathf.Round(moveCost);
                    arrivalDate = dateManager.AddDays(arrivalDate, moveTime);
                    var task = new MoveArmyTask(arrivalDate, Army, destination);

                    backgroundTaskManager.SubmitTask(task);
                }
            }
        }
    }
}