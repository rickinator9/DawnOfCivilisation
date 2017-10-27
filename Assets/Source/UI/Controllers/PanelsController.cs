using Assets.Source.Contexts.Game.Views.UI;
using UnityEngine;

namespace Assets.Source.UI.Controllers
{
    public class PanelsController : MonoBehaviour, IPanels
    {
        public static IPanels Instance { get; private set; }

        public IArmyPanel ArmyPanel
        {
            get { return ArmyPanelController; }
        }

        public ICountryPanel CountryPanel
        {
            get { return CountryPanelController; }
        }

        public IHexPanel HexPanel
        {
            get { return HexPanelView; }
        }

        private IPanel[] AllPanels
        {
            get { return new IPanel[] {ArmyPanel, CountryPanel, HexPanel}; }
        }

        #region Unity Properties
        public ArmyPanelController ArmyPanelController;
        public CountryPanelController CountryPanelController;
        public HexPanelView HexPanelView;
        #endregion

        public void HideAll()
        {
            foreach (var panel in AllPanels)
            {
                panel.Hide();
            }
        }

        void Awake()
        {
            Instance = this;
        }
    }
}