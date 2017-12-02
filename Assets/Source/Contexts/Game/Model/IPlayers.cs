using System.Collections.Generic;
using Assets.Source.Model;
using JetBrains.Annotations;
using strange.extensions.signal.impl;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IPlayers
    {
        ILocalPlayer LocalPlayer { get; set; }

        IList<Signal<IPlayer>> LocalPlayerChangeSignals { get; }
    }

    class Players : IPlayers
    {
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
    }
}