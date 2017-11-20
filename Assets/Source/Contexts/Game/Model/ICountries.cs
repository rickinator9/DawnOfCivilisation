using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Assets.Source.Contexts.Game.Model
{
    public interface ICountries
    {
        ICountry[] All { get; }

        void Add(ICountry country);

        void Remove(ICountry country);
    }

    public class Countries : ICountries
    {
        private IList<ICountry> _countries = new List<ICountry>(); 
        public ICountry[] All { get { return _countries.ToArray(); } }

        public void Add(ICountry country)
        {
            _countries.Add(country);
        }

        public void Remove(ICountry country)
        {
            _countries.Remove(country);
        }
    }
}