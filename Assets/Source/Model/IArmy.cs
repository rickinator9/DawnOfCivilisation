using Assets.Source.Views;
using UnityEngine;

namespace Assets.Source.Model
{
    public interface IArmy
    {
        IHexTile Location { get; set; }

        Vector3 Position { get; }

        IArmyView View { get; set; }

        bool HasView { get; }
    }
}