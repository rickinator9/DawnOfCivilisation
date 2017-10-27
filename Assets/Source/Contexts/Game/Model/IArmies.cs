using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Contexts.Game.Model
{
    public interface IArmies
    {
        IArmy[] AllArmies { get; }

        void AddArmy(IArmy army);
    }

    public class Armies : IArmies
    {
        private IList<IArmy> _armies = new List<IArmy>();

        public IArmy[] AllArmies
        {
            get { return _armies.ToArray(); }
        }

        public void AddArmy(IArmy army)
        {
            _armies.Add(army);
        }
    }
}