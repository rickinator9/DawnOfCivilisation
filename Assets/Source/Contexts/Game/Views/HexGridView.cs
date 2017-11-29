using Assets.Source.Contexts.Game.Model.Map;
using Assets.Source.Contexts.Game.Model.Map.MapMode;
using strange.extensions.mediation.impl;

namespace Assets.Source.Contexts.Game.Views
{
    public class HexGridView : View
    {

        public HexMesh Mesh;

        public bool NeedsRefresh { get; set; }

        public IHexTile[] Tiles { get; set; }

        public IMapMode MapMode { get; set; }

        protected void Update()
        {
            if (NeedsRefresh && MapMode != null)
            {
                Mesh.Triangulate(Tiles, MapMode);
                NeedsRefresh = false;
            }
        }
    }
}