using System.Collections.Generic;
using System.Linq;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Model.Player
{
    public interface IPlayers
    {
        IPlayer[] All { get; }

        ILocalPlayer LocalPlayer { get; set; }

        IList<Signal<IPlayer>> LocalPlayerChangeSignals { get; }

        void Add(IPlayer player);
        void Remove(IPlayer player);
    }

    class Players : IPlayers
    {
        private IList<IPlayer> _players = new List<IPlayer>(); 
        public IPlayer[] All { get { return _players.ToArray(); } }

        private ILocalPlayer _localPlayer;
        public ILocalPlayer LocalPlayer
        {
            get { return _localPlayer; }
            set
            {
                _localPlayer = value;
                foreach (var signal in LocalPlayerChangeSignals) { signal.Dispatch(_localPlayer); }
            }
        }

        private IList<Signal<IPlayer>> _localPlayerChangeSignals = new List<Signal<IPlayer>>(); 
        public IList<Signal<IPlayer>> LocalPlayerChangeSignals { get {return _localPlayerChangeSignals;} }

        public void Add(IPlayer player) { _players.Add(player); }
        public void Remove(IPlayer player) { _players.Remove(player); }
    }
}