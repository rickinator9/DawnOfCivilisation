using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Hex;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IArmy : ISelectable, IMovable
    {
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

        public void OnArrivalInTile(IHexTile tile)
        {
            if (tile.TerrainType == HexTerrainType.Water) return;

            var landTile = (ILandTile) tile;
            landTile.Controller = Country;
        }

        public Vector3 Position
        {
            get { return Location.Center; }
        }

        private IList<Signal> _refreshSignals = new List<Signal>(); 
        public IList<Signal> RefreshSignals { get { return _refreshSignals; } }

        public ICountry Country { get; set; }
        public IMovementPath MovementPath { get; set; }
        public bool IsMoving { get; set; }
    }
}