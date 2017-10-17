﻿using Assets.Source.Model.Background.Impl;
using Assets.Source.Views;
using UnityEngine;

namespace Assets.Source.Model.Impl
{
    public class Army : IArmy
    {
        private IHexTile _location;

        public IHexTile Location
        {
            get { return _location; }
            set
            {
                if(_location != null) _location.RemoveArmy(this);
                value.AddArmy(this);

                _location = value;
                
                if(HasView) View.Refresh();
            }
        }

        public Vector3 Position
        {
            get { return Location.Center; }
        }

        private IArmyView _view;
        public IArmyView View
        {
            get
            {
                return _view;
            }
            set
            {
                if (HasView) return; // Don't set if it army exists.

                _view = value;
                _view.Initialise(this);
            }
        }

        public bool HasView
        {
            get { return View != null; }
        }

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
                    arrivalDate = dateManager.AddDays(arrivalDate, 1);
                    var destination = tilePath[i];
                    var task = new MoveArmyTask(arrivalDate, this, destination);

                    backgroundTaskManager.SubmitTask(task);
                }
            }
        }
    }
}