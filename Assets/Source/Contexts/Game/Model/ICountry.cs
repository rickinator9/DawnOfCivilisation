using System.Collections.Generic;
using System.Linq;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Utils;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model
{
    public interface ICountry : IMovable, ISelectable
    {
        string Name { get; set; }

        Color Color { get; }

        ILandTile[] Territories { get; }

        bool IsPlayerControlled { get; set; }
    }

    public class Country : ICountry
    {
        public string Name { get; set; }

        private bool _colorInitialised = false;
        private Color _color;
        public Color Color
        {
            get
            {
                if (!_colorInitialised)
                {
                    _colorInitialised = true;
                    _color = ColorUtils.GetRandomColor();
                }

                return _color;
            }
        }

        public ILandTile[] Territories
        {
            get
            {
                if (Location == null) return new ILandTile[0];

                var list = new List<ILandTile>();
                list.Add(_location as ILandTile);
                foreach (var neighbor in Location.Neighbors)
                {
                    if(neighbor == null || neighbor.TerrainType == HexTerrainType.Water) continue;

                    list.Add(neighbor as ILandTile);
                }

                return list.ToArray();
            }
        }

        public bool IsPlayerControlled { get; set; }

        public IMovementPath MovementPath { get; set; }

        public bool IsMoving { get; set; }

        private IHexTile _location;
        public IHexTile Location
        {
            get { return _location; }
            set
            {
                if (_location != null)
                {
                    foreach (var territory in Territories)
                    {
                        territory.Country = null;
                        territory.Controller = null;
                    }
                }
                _location = value;
                foreach (var territory in Territories)
                {
                    territory.Country = this;
                    territory.Controller = this;
                }
            }
        }

        public void OnArrivalInTile(IHexTile tile)
        {
            Debug.Log("Country arrived in tile.");
            Location = tile;
        }
    }
}