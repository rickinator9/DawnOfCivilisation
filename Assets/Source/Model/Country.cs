using UnityEngine;

namespace Assets.Source.Model
{
    public class Country : ICountry
    {
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
    }
}