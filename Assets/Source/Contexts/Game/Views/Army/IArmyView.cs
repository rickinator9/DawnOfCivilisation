using strange.extensions.mediation.api;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Views.Army
{
    public interface IArmyView : IView
    {
        Vector3 Position { set; }

        Color Color { set; }
    }
}
