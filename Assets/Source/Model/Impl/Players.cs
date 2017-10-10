﻿namespace Assets.Source.Model.Impl
{
    class Players : IPlayers
    {
        private static IPlayers _instance;
        public static IPlayers Instance
        {
            get { return _instance ?? (_instance = new Players()); }
        }

        public IPlayer CurrentPlayer { get; set; }

        private Players()
        {
            
        }
    }
}
