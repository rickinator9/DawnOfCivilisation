using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class CountryPanelController : MonoBehaviour, ICountryPanel
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

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowForCountry(ICountry country)
        {
            Country = country;
            Show();
        }
    }
}