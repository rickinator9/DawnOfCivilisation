using Assets.Source.Contexts.Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.ArmyPanel
{
    public class ArmyPanelView : TypedUiView<IArmy>, IArmyPanelView
    {
        public string CountryName
        {
            get { return _countryNameText.text; }
            set { _countryNameText.text = value; }
        }

        #region Unity Components
        [SerializeField]
        private Text _countryNameText;
        #endregion
    }
}