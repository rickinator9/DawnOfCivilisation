using System;
using Assets.Source.Contexts.Game.Model.Hex;

namespace Assets.Source.Contexts.Game.Model
{
    public interface ICity
    {
        string Name { get; set; }

        int Population { get; }

        IHexTile Location { get; }

        ICountry Country { get; }

        void Initialise(IHexTile location, string name);
    }

    public class City : ICity
    {
        public string Name { get; set; }

        public int Population
        {
            get { throw new NotImplementedException(); }
        }

        public IHexTile Location { get; private set; }

        public ICountry Country
        {
            get { return Location.Country; }
        }

        public void Initialise(IHexTile location, string name)
        {
            Location = location;
            Name = name;
        }
    }
}