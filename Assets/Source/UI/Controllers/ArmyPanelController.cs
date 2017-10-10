using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class ArmyPanelController : MonoBehaviour, IArmyPanel
    {
        private IArmy _army;

        public IArmy Army
        {
            get { return _army; }
            set
            {
                _army = value;

                CountryValue.text = _army.Country.Name;
            }
        }

        public Text CountryValue;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowForArmy(IArmy army)
        {
            Army = army;

            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}