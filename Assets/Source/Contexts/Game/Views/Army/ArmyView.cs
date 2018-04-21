using Assets.Source.Contexts.Game.Model;
using Assets.Source.Contexts.Game.Views.Army;
using Assets.Source.Core.IoC;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Views.Army
{
    public class ArmyView : View, IArmyView
    {
        public Vector3 Position
        {
            set { transform.localPosition = value + new Vector3(0, transform.localScale.y / 2, 0); }
        }

        private Renderer _renderer;
        public Color Color
        {
            set {
                if (_renderer == null) _renderer = GetComponent<MeshRenderer>();

                var material = _renderer.material;
                material.SetColor("_Color", value);
            }
        }
    }
}