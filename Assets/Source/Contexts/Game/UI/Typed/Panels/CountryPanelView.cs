using Assets.Source.Model;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CountryPanelView : TypedUiView<ICountry>
    {
        private ICountry _country;
        public ICountry Country
        {
            get { return _country; }
            set
            {
                _country = value;
                CountryNameText.text = _country.Name;
            }
        }

        public Text CountryNameText;

        public override void UpdateValues(ICountry obj)
        {
            Country = obj;
        }
    }
}