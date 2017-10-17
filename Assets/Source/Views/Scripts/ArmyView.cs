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

        private bool NeedsRefresh { get; set; }

        public void Initialise(IArmy army)
        {
            Army = army;

            Refresh();
        }

        void Update()
        {
            if(NeedsRefresh) OnRefresh();
        }

        public void Refresh()
        {
            NeedsRefresh = true;
        }

        private void OnRefresh()
        {
            Position = Army.Position;

            NeedsRefresh = false;
        }
    }
}