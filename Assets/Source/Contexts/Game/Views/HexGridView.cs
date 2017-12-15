using System.Collections.Generic;
using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using Assets.Source.Contexts.Game.Model.Map.Mesh;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.Views
{
    public class HexGridView : View
    {

        public List<BaseHexMesh> Meshes; 

        public bool NeedsRefresh { get; set; }

        public IHexTile[] Tiles { get; set; }

        public IMapMode MapMode { get; set; }

        protected void Update()
        {
            if (NeedsRefresh && MapMode != null)
            {
                foreach (var mesh in Meshes)
                {
                    mesh.Triangulate(Tiles, MapMode);
                }
                NeedsRefresh = false;
            }
        }
    }
}