using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Hex;
using Assets.Source.Model;
using Assets.Source.Model.Background.Impl;
using Assets.Source.Model.Impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IArmy : ISelectable
    {
        IHexTile Location { get; set; }

        Vector3 Position { get; }

        IList<Signal> RefreshSignals { get; }

        ICountry Country { get; set; }
    }

    public class Army : IArmy
    {
        private IHexTile _location;

        public IHexTile Location
        {
            get { return _location; }
            set
            {
                if (_location != null) _location.RemoveArmy(this);
                value.AddArmy(this);

                _location = value;

                foreach (var signal in RefreshSignals)
                {
                    signal.Dispatch();
                }
            }
        }

        public Vector3 Position
        {
            get { return Location.Center; }
        }

        private IList<Signal> _refreshSignals = new List<Signal>(); 
        public IList<Signal> RefreshSignals { get { return _refreshSignals; } }

        public ICountry Country { get; set; }

        public void OnRightClickOnTile(IHexTile tile)
        {
            if (Location != tile)
            {
                var grid = HexGrid.Instance;
                var tilePath = grid.FindPath(Location, tile);

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
                    var task = new MoveArmyTask(arrivalDate, this, destination);

                    backgroundTaskManager.SubmitTask(task);
                }
            }
        }
    }
}