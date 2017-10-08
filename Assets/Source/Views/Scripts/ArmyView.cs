using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Views.Scripts
{
    public class ArmyView : MonoBehaviour, IArmyView
    {
        private Vector3 Position
        {
            get { return transform.localPosition; }
            set { transform.localPosition = value + new Vector3(0, transform.localScale.y/2, 0); }
        }

        private IArmy Army { get; set; }

        public void Initialise(IArmy army)
        {
            Army = army;

            Refresh();
        }

        public void Refresh()
        {
            Position = Army.Position;
        }
    }
}