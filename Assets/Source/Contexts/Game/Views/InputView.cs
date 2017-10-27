using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Source.Contexts.Game.Views
{
    public class InputView : View
    {
        public Signal LeftMouseClickSignal = new Signal();
        public Signal RightMouseClickSignal = new Signal();

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if(Input.GetMouseButtonDown(0)) LeftMouseClickSignal.Dispatch();
            else if(Input.GetMouseButtonDown(1)) RightMouseClickSignal.Dispatch();
        }
    }
}