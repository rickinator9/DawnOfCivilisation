using UnityEngine;

namespace Assets.Source.Contexts.Game.Model.Country
{
    public interface ICountryNames
    {
        string[] AllNames { get; }

        string RandomName { get; }
    }

    public class CountryNames : ICountryNames
    {
        public string[] AllNames
        {
            get { return new[] { "Sumeria", "Akkad", "Elam", "Kemet", "Hattusa", "Lydia"}; }
        }

        public string RandomName
        {
            get
            {
                var index = (int)((AllNames.Length-1)*Random.value);
                return AllNames[index];
            }
        }
    }
}