using Assets.Source.Views;
using UnityEngine;

namespace Assets.Source.Model
{
    public class Army : IArmy
    {
        private IHexTile _location;

        public IHexTile Location
        {
            get { return _location; }
            set
            {
                _location = value;
                if(HasView) View.Refresh();
            }
        }

        public Vector3 Position
        {
            get { return Location.Center; }
        }

        private IArmyView _view;
        public IArmyView View
        {
            get
            {
                return _view;
            }
            set
            {
                if (HasView) return; // Don't set if it army exists.

                _view = value;
                _view.Initialise(this);
            }
        }

        public bool HasView
        {
            get { return View != null; }
        }
    }
}