using Assets.Source.Contexts.Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CityPanelView : TypedUiView<ICity>
    {
        [SerializeField] private Text _nameText, _countryText, _populationText;

        public override void UpdateValues(ICity city)
        {
            _nameText.text = city.Name;
            _countryText.text = city.Country != null ? city.Country.Name : "None";
            _populationText.text = city.Population.ToString();
        }
    }
}