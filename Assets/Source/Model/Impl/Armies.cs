using System.Collections.Generic;
using System.Linq;
using Assets.Source.Contexts.Model;

namespace Assets.Source.Model.Impl
{
    public class Armies : IArmies
    {
        private static IArmies _instance;
        public static IArmies Instance
        {
            get
            {
                if(_instance == null)
                   _instance = new Armies();

                return _instance;
            }
        }

        private IList<IArmy> _armies;

        public IArmy[] AllArmies
        {
            get { return _armies.ToArray(); }
        }

        private Armies()
        {
            _armies = new List<IArmy>();
        }

        public IArmy CreateArmy()
        {
            var army = new Army();
            _armies.Add(army);

            return army;
        }
    }
}