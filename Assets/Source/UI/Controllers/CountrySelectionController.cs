using Assets.Source.Model.Impl;
using UnityEngine;

namespace Assets.Source.UI.Controllers
{
    public class CountrySelectionController : MonoBehaviour {

        public void OnCountrySelected()
        {
            var panels = PanelsController.Instance;
            panels.HideAll();
            panels.CountryPanel.ShowForCountry(Players.Instance.LocalPlayer.Country);
        }
    }
}
