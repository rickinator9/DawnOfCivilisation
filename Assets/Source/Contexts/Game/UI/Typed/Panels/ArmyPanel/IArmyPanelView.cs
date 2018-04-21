using Assets.Source.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels.ArmyPanel
{
    public interface IArmyPanelView : IDOCView
    {
        string CountryName { set; }
    }
}
