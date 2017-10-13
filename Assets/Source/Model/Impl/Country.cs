using UnityEngine;

namespace Assets.Source.Model.Impl
{
    public class Country : ICountry
    {
        public string Name { get; set; }

        private bool _colorInitialised = false;
        private Color _color;
        public Color Color
        {
            get
            {
                if (!_colorInitialised)
                {
                    _colorInitialised = true;
                    _color = Color.red; // TODO: Make random.
                }

                return _color;
            }
        }

        public Country(string name)
        {
            Name = name;
        }
    }
}