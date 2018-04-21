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
        [SerializeField]
        private Text CountryNameText;
        [SerializeField]
        private Button DeclareWarButton;

        public string CountryName
        {
            get { return CountryNameText.text; }
            set { CountryNameText.text = value; }
        }

        public bool CanDeclareWar
        {
            get { return DeclareWarButton.gameObject.activeSelf; }
            set { DeclareWarButton.gameObject.SetActive(value); }
        }

        public Signal DeclareWarSignal = new Signal();

        // Unity Listeners
        public void ButtonDeclareWar()
        {
            DeclareWarSignal.Dispatch();
        }
    }
}