using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Model;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class CountryPanelView : TypedUiView<ICountry>
    {
        private ICountry _country;

        [SerializeField]
        private Text CountryNameText;
        [SerializeField]
        private Button DeclareWarButton;

        public Signal<ICountry> DeclareWarSignal = new Signal<ICountry>();

        public override void UpdateValues(ICountry obj)
        {
            _country = obj;
            CountryNameText.text = _country.Name;
            DeclareWarButton.gameObject.SetActive(!_country.IsPlayerControlled);
        }

        // Unity Listeners
        public void ButtonDeclareWar()
        {
            DeclareWarSignal.Dispatch(_country);
        }
    }
}