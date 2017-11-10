using Assets.Source.Contexts.Game.Model;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.Views
{
    public class CityWorldPanelView : View
    {
        [SerializeField] private Text _cityNameField;


        public Signal OnClickSignal = new Signal();

        public void OnViewClick()
        {
            OnClickSignal.Dispatch();
        }

        public void UpdateUi(ICity city)
        {
            _cityNameField.text = city.Name;
        }

        protected void Update()
        {
            //var lookAt = Quaternion.LookRotation()
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation*Vector3.up);
        }
    }
}