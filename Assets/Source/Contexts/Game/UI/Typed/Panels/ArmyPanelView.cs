using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.UI.Typed;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Controllers
{
    public class ArmyPanelView : TypedUiView<IArmy>
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

        public override void UpdateValues(IArmy obj)
        {
            Army = obj;
        }
    }
}