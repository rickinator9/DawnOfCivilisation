using Assets.Source.Contexts.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Views
{
    public class ArmyView : View
    {
        private Vector3 Position
        {
            get { return transform.localPosition; }
            set { transform.localPosition = value + new Vector3(0, transform.localScale.y/2, 0); }
        }

        public void OnRefresh(IArmy army)
        {
            Position = army.Position;
        }
    }
}