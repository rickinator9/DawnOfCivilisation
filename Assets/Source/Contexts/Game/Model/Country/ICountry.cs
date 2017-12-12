using System.Collections.Generic;
using System.Linq;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Player;
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Utils;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Country
{
    public interface ICountry : IMovable, ISelectable
    {
        string Name { get; set; }

        Color Color { get; }

        IPlayer Player { get; set; }

        bool HasPlayer { get; }

        bool IsHumanControlled { get; }

        ILandTile[] Territories { get; }

        IArmy[] Armies { get; }

        void AddArmy(IArmy army);

        void RemoveArmy(IArmy army);

        IWar[] Wars { get; }

        IList<Signal<IWar>> WarAddedSignals { get; }
        
        IList<Signal<IWar>> WarRemovedSignals { get; }  

        void AddWar(IWar war);

        void RemoveWar(IWar war);

        bool IsEnemyOfCountry(ICountry country);

        bool IsFriendOfCountry(ICountry country);
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

        public IPlayer Player { get; set; }

        public bool HasPlayer { get { return Player != null; } }

        public bool IsHumanControlled
        {
            get
            {
                if (!HasPlayer) return false;

                return Player.Type == PlayerType.Human;
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

        private IList<IArmy> _armies = new List<IArmy>(); 
        public IArmy[] Armies { get { return _armies.ToArray(); } }
        public void AddArmy(IArmy army) { _armies.Add(army); }
        public void RemoveArmy(IArmy army) { _armies.Remove(army); }

        private IList<IWar> _wars = new List<IWar>(); 
        public IWar[] Wars { get { return _wars.ToArray(); } }

        private IList<Signal<IWar>> _warAddedSignals = new List<Signal<IWar>>(); 
        public IList<Signal<IWar>> WarAddedSignals { get { return _warAddedSignals; } }

        private IList<Signal<IWar>> _warRemovedSignals = new List<Signal<IWar>>(); 
        public IList<Signal<IWar>> WarRemovedSignals { get { return _warRemovedSignals; } }

        public void AddWar(IWar war)
        {
            _wars.Add(war);
            foreach (var signal in WarAddedSignals) { signal.Dispatch(war); }
        }

        public void RemoveWar(IWar war)
        {
            _wars.Remove(war);
            foreach (var signal in WarRemovedSignals) { signal.Dispatch(war); }
        }

        public bool IsEnemyOfCountry(ICountry country)
        {
            if (country == null) return false;

            foreach (var war in Wars)
            {
                foreach (var enemy in war.GetEnemiesOfCountry(this))
                {
                    if (enemy == country) return true;
                }
            }

            return false;
        }

        public bool IsFriendOfCountry(ICountry country)
        {
            if (country == null) return false;

            if (country == this) return true;

            foreach (var war in Wars)
            {
                foreach (var friend in war.GetFriendsOfCountry(this))
                {
                    if (friend == country) return true;
                }
            }

            return false;
        }

        #region IMovable Implementations
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
#endregion
    }
}