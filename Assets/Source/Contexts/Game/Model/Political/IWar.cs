using System.Collections.Generic;
using System.Linq;
using Assets.Source.Contexts.Game.Model.Country;

namespace Assets.Source.Contexts.Game.Model.Political
{
    public interface IWar
    {
        string Name { get; set; }
        
        ICountry[] Attackers { get; }
        
        ICountry[] Defenders { get; }

        void AddAttacker(ICountry attacker);

        void RemoveAttacker(ICountry attacker);

        void AddDefender(ICountry defender);

        void RemoveDefender(ICountry defender);

        bool IsAttacker(ICountry country);

        bool IsDefender(ICountry country);

        ICountry[] GetEnemiesOfCountry(ICountry country);
        ICountry[] GetFriendsOfCountry(ICountry country);
    }

    public class War : IWar
    {
        public string Name { get; set; }

        private IList<ICountry> _attackers = new List<ICountry>();
        public ICountry[] Attackers { get { return _attackers.ToArray(); } }

        private IList<ICountry> _defenders = new List<ICountry>(); 
        public ICountry[] Defenders { get { return _defenders.ToArray(); } }

        public void AddAttacker(ICountry attacker) { _attackers.Add(attacker); }
        public void RemoveAttacker(ICountry attacker) { _attackers.Remove(attacker); }

        public void AddDefender(ICountry defender) { _defenders.Add(defender); }
        public void RemoveDefender(ICountry defender) { _defenders.Remove(defender); }

        public bool IsAttacker(ICountry country) { return _attackers.Contains(country); }
        public bool IsDefender(ICountry country) { return _defenders.Contains(country); }

        public ICountry[] GetEnemiesOfCountry(ICountry country)
        {
            if(IsAttacker(country)) return Defenders;
            if(IsDefender(country)) return Attackers;
            return new ICountry[0]; // Empty, since country is not involved in the war.
        }

        public ICountry[] GetFriendsOfCountry(ICountry country)
        {
            var list = new List<ICountry>();
            ICountry[] friendlyAlliance = null;

            if(IsAttacker(country)) friendlyAlliance = Attackers;
            if(IsDefender(country)) friendlyAlliance = Defenders;

            foreach (var friendlyCountry in friendlyAlliance)
            {
                if(friendlyCountry != country) list.Add(friendlyCountry);
            }

            return list.Count > 0 ? list.ToArray() : new ICountry[0];
        }
    }
}