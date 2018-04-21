using Assets.Source.Contexts.Game.Model;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.Views
{
    public class CityWorldPanelView : View
    {
        public string CityName
        {
            set { _cityNameText.text = value; }
        }

        [SerializeField] private Text _cityNameText;

        public Signal OnClickSignal = new Signal();

        public void OnViewClick()
        {
            OnClickSignal.Dispatch();
        }

        protected void Update()
        {
            //var lookAt = Quaternion.LookRotation()
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation*Vector3.up);
        }
    }
}