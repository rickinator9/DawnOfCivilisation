using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Contexts.Game.Model
{
    public interface ICities
    {
        ICity[] AllCities { get; }

        void Add(ICity city);

        void Remove(ICity city);
    }

    public class Cities : ICities
    {
        private IList<ICity> _cities = new List<ICity>(); 
        public ICity[] AllCities { get { return _cities.ToArray(); } }

        public void Add(ICity city)
        {
            _cities.Add(city);
        }

        public void Remove(ICity city)
        {
            _cities.Remove(city);
        }
    }
}